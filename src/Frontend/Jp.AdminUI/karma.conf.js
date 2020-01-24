// Karma configuration file, see link for more information
// https://karma-runner.github.io/1.0/config/configuration-file.html

module.exports = function (config) {
  const puppeteer = require('puppeteer');
  process.env.CHROME_BIN = puppeteer.executablePath();

  config.set({
    basePath: '',
    frameworks: ['jasmine', '@angular-devkit/build-angular'],
    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'),
      require('karma-jasmine-html-reporter'),
      require('karma-coverage-istanbul-reporter'),
      require('@angular-devkit/build-angular/plugins/karma'),
      require('karma-junit-reporter')
    ],
    client:{
      clearContext: false // leave Jasmine Spec Runner output visible in browser
    },
    files: [
      { pattern: './src/test.ts', watched: false },
      { pattern: 'src/assets/**/*', watched: false, included: false, served: true },
      { pattern: 'src/assets/i18n/*.json', watched: false, included: false, served: true },
    ],
    proxies: {
      '/assets/': '/base/src/assets/'
    },
    angularCli: {
      environment: 'dev'
    },
    coverageIstanbulReporter: {
      dir: require('path').join(__dirname, 'coverage'), reports: [ 'html', 'lcovonly', 'text-summary', 'cobertura' ],
      fixWebpackSourcePaths: true
    },
    reporters: ['progress', 'kjhtml', 'junit'],
    junitReporter: {
      outputDir: '../junit'
    },
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: true,
    browsers: ['ChromeHeadless'],
    singleRun: false,
    restartOnFileChange: true
  });
};
