plugins {
    alias(libs.plugins.kotlin.android)
    alias(libs.plugins.kotlin.compose)
    alias(libs.plugins.android.library)
}

android {
    namespace = "tk.android.timekeeper"
    compileSdk = 36

    defaultConfig {
        minSdk = 21
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(getDefaultProguardFile("proguard-android-optimize.txt"), "proguard-rules.pro")
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_11
        targetCompatibility = JavaVersion.VERSION_11
    }
    kotlinOptions {
        jvmTarget = "11"
    }
    buildFeatures {
        compose = true
    }
}

// If the app crashes with a "class not found", add it in Tk.App.Android.csproj
dependencies {

    implementation(                    libs.androidx.core.ktx)
    implementation(                    libs.androidx.lifecycle.runtime.ktx)
    implementation(                    libs.androidx.activity.compose)
    implementation(           platform(libs.androidx.compose.bom))
    implementation(                    libs.androidx.compose.ui)
    implementation(                    libs.androidx.compose.ui.graphics)
    implementation(                    libs.androidx.compose.ui.tooling.preview)
    implementation(                    libs.androidx.compose.material3)
    testImplementation(                libs.junit)
    androidTestImplementation(         libs.androidx.junit)
    androidTestImplementation(         libs.androidx.espresso.core)
    androidTestImplementation(platform(libs.androidx.compose.bom))
    androidTestImplementation(         libs.androidx.compose.ui.test.junit4)
    debugImplementation(               libs.androidx.compose.ui.tooling)
    debugImplementation(               libs.androidx.compose.ui.test.manifest)
    
    // implementation("androidx.activity:activity-ktx")
    // implementation("androidx.lifecycle:lifecycle-viewmodel-compose")
    // implementation("org.jetbrains.kotlin:kotlin-gradle-plugin")


    val nav_version = "2.9.5"

    // Jetpack Compose integration
    implementation("androidx.navigation:navigation-compose:$nav_version")

    // Views/Fragments integration
    implementation("androidx.navigation:navigation-fragment:$nav_version")
    implementation("androidx.navigation:navigation-ui:$nav_version")

    // Feature module support for Fragments
    implementation("androidx.navigation:navigation-dynamic-features-fragment:$nav_version")

    // Testing Navigation
    androidTestImplementation("androidx.navigation:navigation-testing:$nav_version")

    // JSON serialization library, works with the Kotlin serialization plugin
    implementation("org.jetbrains.kotlinx:kotlinx-serialization-json:1.7.3")


    implementation("androidx.compose.material:material-icons-core:1.7.8")
    implementation("androidx.compose.material:material-icons-extended:1.7.8")

}
