
// https://github.com/angular/protractor/blob/master/lib/config.ts
const { SpecReporter } = require('jasmine-spec-reporter');
const { JUnitXmlReporter } = require('jasmine-reporters');
process.env.CHROME_BIN = require("puppeteer").executablePath();
exports.config = {
  allScriptsTimeout: 30000,
    './*.e2e-spec.ts'
  ],
  capabilities: {
    'browserName': 'chrome',
    chromeOptions: {
      args: ["--headless", "--disable-gpu", "--window-size=1200,900"],
      binary: process.env.CHROME_BIN
    }
  },
  directConnect: true,
  baseUrl: 'http://localhost:4200/',
      project: 'e2e/tsconfig.e2e.json'
    });
    jasmine.getEnv().addReporter(new SpecReporter({ spec: { displayStacktrace: true } }));
    var junitReporter = new JUnitXmlReporter({
      savePath: require('path').join(__dirname, './junit'),
      consolidateAll: true
    });
    jasmine.getEnv().addReporter(junitReporter);
  }
};
