import 'package:flutter/material.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:sqflite_common_ffi/sqflite_ffi.dart';
import 'package:timekeeper/src/rust_lib/frb_api.dart';
import 'package:timekeeper/src/rust_lib/frb_api/state.dart';
import 'package:timekeeper/src/rust_lib/models/task.dart';
import 'package:timekeeper/src/rust_lib/frb_generated.dart';
import 'package:path/path.dart';
import 'dart:io';
import 'package:english_words/english_words.dart';



import 'package:styled_widget/styled_widget.dart';


Future<void> main() async {
  await dotenv.load(fileName: '.env');

  runRustApp(body: body, state: RustState.new);
}


Widget body(RustState state) {
  return MaterialApp(
    title: 'Timekeeper',
    theme: ThemeData(
      colorScheme: ColorScheme.fromSeed(seedColor: const Color.fromARGB(255, 71, 167, 212)),
    ),
    home: homePage(state),
  );
}


Widget homePage(RustState state) {

  IconData icon = state.isFavourited()? Icons.favorite : Icons.favorite_border;

  return Scaffold(
    body: Center(
      child: [

        Text('A random idea:'), 
        BigCard(words: state.currentWord),
        SizedBox(height: 10,),

        [
          ElevatedButton.icon(
            onPressed: () {
              state.toggleFavouriteWord();
            },
            icon:  Icon(icon),
            label: Text('Like'),
          ),

          ElevatedButton(
            onPressed: () {
              getNextWord(state);
            },
            child: Text('Next'),
          ),
        ].toRow(
          mainAxisSize: MainAxisSize.min
        )
        
      ].toColumn(
        mainAxisAlignment: MainAxisAlignment.center,
      ),
    )
  );
}

void getNextWord(RustState state) {
  final pair = WordPair.random();
  state.setWord(wordPair: "${pair.first} ${pair.second}");
}


class BigCard extends StatelessWidget {
  const BigCard({super.key, required this.words});

  final String words;

  @override
  Widget build(BuildContext context) {

    final theme = Theme.of(context);

    final style = theme.textTheme.displayMedium!.copyWith(
      color: theme.colorScheme.onPrimary,
    );

    return Card(
      color: theme.colorScheme.primary,
      child: Padding(
        padding: const EdgeInsets.all(20),
        child:   Text(
          words, 
          style: style,
          // semanticsLabel: "${pair.first} ${pair.second}",
        ),
      ),
    );
  }
}

Widget body_(RustState state) {

  return [
    SyncTextField(
      decoration:  InputDecoration(hintText: 'Input text and enter to add a todo'),
      text:        state.inputText,
      onChanged:   (text) => state.inputText = text,
      onSubmitted: (_)    => state.add(),
    ).padding(bottom: 8),

    ListView(children: [
      for (final item in state.filteredItems()) todoItem(state, item)
    ]).expanded(),

    [
      for (final filter in Filter.values)
        TextButton(
          onPressed: () => state.filter = filter,

          child: Text(filter.name)
            .textColor(state.filter == filter ? Colors.blue : Colors.black87),
        ),
    ].toRow(),
  ]
    .toColumn()
    .padding(all: 16)
  ;
}

Widget todoItem(RustState state, Task task) {

  return [
    Checkbox(
      value:     !task.isActive,
      onChanged: (_) => state.toggle(id: task.id)
    ),

    Text(task.name).expanded(),

    IconButton(
        icon:      Icon(Icons.close, color: Colors.grey),
        onPressed: () => state.remove(id: task.id)),
  ].toRow();
}


Future initDb() async {

  if (isDesktop()) {
    databaseFactory = databaseFactoryFfi;
  }

  var url = await dbUrl();
  
  await connectDb(dbUrl: url);
}

Future<String> dbUrl() async {

  if (isMobile()) {
    return join(await getDatabasesPath(), 'timekeeper.db');
  }


  var dbUrl = dotenv.env['DB_URL'];

  if (dbUrl is! String) {
    throw Exception('env var "DB_URL" is not defined');
  }

  return dbUrl;
}

bool isMobile() {
  return Platform.isIOS || Platform.isAndroid;
}

bool isDesktop() {
  return !isMobile();
}

class SyncTextField extends StatefulWidget {

  final String text;

  // forward
  final ValueChanged<String>? onChanged;
  final InputDecoration?      decoration;
  final ValueChanged<String>? onSubmitted;

  const SyncTextField({
    super.key,
    required this.text,
             this.onChanged,
             this.decoration,
             this.onSubmitted,
  });

  @override
  State<SyncTextField> createState() => _SyncTextFieldState();
}

class _SyncTextFieldState extends State<SyncTextField> {

  late final TextEditingController _controller;

  @override
  void initState() {
    super.initState();

    _controller = TextEditingController();
    _controller.text = widget.text;
  }

  // https://github.com/fzyzcjy/flutter_rust_bridge/issues/2823
  @override
  void didUpdateWidget(covariant SyncTextField oldWidget) {
    super.didUpdateWidget(oldWidget);

    if (_controller.text != widget.text) {
      _controller.text      = widget.text;
      _controller.selection = TextSelection.collapsed(offset: -1);
    }
  }

  @override
  void dispose() {

    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {

    return TextField(
      controller:  _controller,
      // forward
      onChanged:   widget.onChanged,
      decoration:  widget.decoration,
      onSubmitted: widget.onSubmitted,
    );
  }
}