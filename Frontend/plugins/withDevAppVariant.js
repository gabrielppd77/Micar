const { withAppBuildGradle } = require("expo/config-plugins");

const DEBUG_BLOCK = /debug\s*\{\s*signingConfig signingConfigs\.debug\s*\}/;

const withDevAppVariant = (config) => {
  return withAppBuildGradle(config, (config) => {
    if (config.modResults.language !== "groovy") {
      throw new Error(
        "withDevAppVariant only supports Groovy build.gradle files",
      );
    }

    if (!DEBUG_BLOCK.test(config.modResults.contents)) {
      throw new Error(
        "withDevAppVariant could not find the debug buildType block to patch",
      );
    }

    config.modResults.contents = config.modResults.contents.replace(
      DEBUG_BLOCK,
      `debug {
            signingConfig signingConfigs.debug
            applicationIdSuffix ".dev"
            versionNameSuffix "-dev"
            resValue "string", "app_name", "Micar Dev"
        }`,
    );

    return config;
  });
};

module.exports = withDevAppVariant;
