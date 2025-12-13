use std::{env, path::PathBuf};



pub fn get_app_data_dir() -> PathBuf {
    let config_dir = env::var("XDG_CONFIG").unwrap_or_else(|_|
        format!("{}/.config/", env::var("HOME").expect("unable to get config dir"))
    );

    let mut config_dir = PathBuf::from(config_dir);
    config_dir.push("timekeeper/");

    config_dir
}
