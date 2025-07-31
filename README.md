# timekeeper

A todo app for my brain

## setup

```sh
cargo install flutter_rust_bridge_codegen

# watches your rust/dart code and automatically generates FFI glue code
flutter_rust_bridge_codegen generate --watch
```

then simply run with

```sh
cd flutter/

flutter run
```

### Environ

- `rust/.env`
- `flutter/.env`

```sh
DB_URL="/path/to/db/on/PC"
```