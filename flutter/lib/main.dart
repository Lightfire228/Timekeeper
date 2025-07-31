import 'package:flutter/material.dart';
import 'package:flutter_dotenv/flutter_dotenv.dart';
import 'package:sqflite_common_ffi/sqflite_ffi.dart';
import 'package:timekeeper/src/rust_lib/frb_api.dart';
import 'package:timekeeper/src/rust_lib/frb_generated.dart';
import 'package:path/path.dart';
import 'dart:io';

Future<void> main() async {
  await RustLib.init();
  await dotenv.load(fileName: '.env');

  await initDb();
  
  runApp(const MyApp());
}



class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('flutter_rust_bridge quickstart')),
        body: Center(
          child: Text(
            'Action: Call Rust `greet("Tom")`\nResult: `${greet(name: "Tom")}`',
          ),
        ),
      ),
    );
  }
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