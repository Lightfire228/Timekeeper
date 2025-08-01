import 'package:flutter/material.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:sqflite_common_ffi/sqflite_ffi.dart';
import 'package:timekeeper/src/rust_lib/frb_api.dart';
import 'package:timekeeper/src/rust_lib/frb_api/state.dart';
import 'package:timekeeper/src/rust_lib/frb_generated.dart';
import 'package:path/path.dart';
import 'dart:io';


import 'package:styled_widget/styled_widget.dart';


Future<void> main() async {
  // await RustLib.init();
  await dotenv.load(fileName: '.env');

  runRustApp(body: body, state: RustState.new);
}


Widget body(RustState state) {

  return [
    SyncTextField(
      decoration:  InputDecoration(hintText: 'Input text and enter to add a todo'),
      text:        state.inputText,
      onChanged:   (text) => state.inputText += text,
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

Widget todoItem(RustState state, Item item) {

  return [
    Checkbox(
      value:     item.completed,
      onChanged: (_) => state.toggle(id: item.id)
    ),

    Text(item.content).expanded(),

    IconButton(
        icon:      Icon(Icons.close, color: Colors.grey),
        onPressed: () => state.remove(id: item.id)),
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