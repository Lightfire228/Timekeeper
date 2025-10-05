plugins {
    alias(libs.plugins.kotlin.jvm) 
    `java-library` 
}

repositories {
    mavenCentral() 
}

dependencies {
    testImplementation("org.jetbrains.kotlin:kotlin-test") 

    testImplementation(libs.junit.jupiter.engine) 

    testRuntimeOnly("org.junit.platform:junit-platform-launcher")

}