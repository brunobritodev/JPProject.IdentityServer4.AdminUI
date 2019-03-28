// instanciando módulos
var gulp = require('gulp');
var uglify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');    // Creates source map files (https://www.npmjs.com/package/gulp-sourcemaps/)
var replace = require('gulp-replace-task');     // String replace (https://www.npmjs.com/package/gulp-replace-task/)
var concat = require('gulp-concat');
var clean = require('gulp-clean');

//var watch = require('gulp-watch');
var gutil = require('gulp-util');

var cssmin = require('gulp-cssmin');

gulp.task('clean-css', function () {
    return gulp
        .src(["./wwwroot/css/jp.min.css"])
        .pipe(clean())
});

gulp.task('clean-js', function () {
    return gulp
        .src(["./wwwroot/js/jp.min.css"])
        .pipe(clean())
});

gulp.task('css', ['clean-css'],function () {
    gulp.src([
        "./assets/css/coreui-icons.min.css",
        "./assets/css/flag-icon.min.css",
        "./assets/css/font-awesome.min.css",
        "./assets/css/simple-line-icons.min.css",
        "./assets/css/style.css",
        "./assets/css/pace.min.css",
    ])
        .pipe(cssmin())
        .pipe(concat('jp.min.css'))
        .pipe(gulp.dest('./wwwroot/css/'));
});

gulp.task('scripts-commom', ['clean-js'], function () {
    gulp.src([
        "./Assets/js/jquery.min.js",
        "./Assets/js/bootstrap.min.js",
        "./Assets/js/popper.min.js",
        "./Assets/js/pace.min.js",
        "./Assets/js/perfect-scrollbar.min.js",
        "./Assets/js/coreui.min.js",
        "./Assets/js/popper-utils.min.js",
        "./Assets/js/buttons.js",
        ])
        .pipe(concat('jp.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest("./wwwroot/js"));
});

gulp.task('build-app-insights-js',
    function() {
        return gulp
            .src('./node_modules/applicationinsights-js/JavaScript/JavaScriptSDK/snippet.js')
            .pipe(
                replace({                          // Carry out the specified find and replace.
                    patterns: [
                        {
                            // match - The string or regular expression to find.
                            match: 'CDN_PATH',
                            // replacement - The string or function used to make the replacement.
                            replacement: 'https://az416426.vo.msecnd.net/scripts/a/ai.0.js'
                        },
                        {
                           match: 'INSTRUMENTATION_KEY',
                           replacement: '205fabcd-09ee-46f5-bb74-95780fc873da'
                        }
                    ],
                    usePrefix: false
                }))
            .pipe(uglify())                        // Minifies the JavaScript.
            .pipe(concat('app.insights.js'))
            .pipe(gulp.dest('./wwwroot/js'));     // Saves the JavaScript file to the specified destination path.
});