use std::{fs, io::ErrorKind, path::PathBuf};

// https://stackoverflow.com/a/75327986/2716305
#[cfg(any(target_os = "android", rust_analyzer))]
mod android;

#[cfg(any(target_os = "linux", rust_analyzer))]
mod linux;

macro_rules! platform {
    ($linux: expr, $android: expr,) => {
        {
            #[cfg(target_os = "linux")]
            return $linux;
    
            #[cfg(target_os = "android")]
            return $android;
        }
    };
}


pub fn init_files() {

    let path = get_app_data_dir();

    fs::create_dir(&path)
        .or_else(|err| match err.kind() {
            ErrorKind::AlreadyExists => Ok(()),
            _                        => Err(err)
        })
        .expect("Unable to create app dir")
    ;
}

pub fn get_app_data_dir() -> PathBuf {
    platform!(
        linux  ::get_app_data_dir(),
        android::get_app_data_dir(),
    );
}
