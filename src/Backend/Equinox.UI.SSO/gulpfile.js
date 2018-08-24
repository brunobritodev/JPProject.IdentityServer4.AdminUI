// instanciando módulos
var gulp = require('gulp');
var uglify = require('gulp-uglify');
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

gulp.task('css', ['clean-css'],function () {
    gulp.src([
        "./assets/css/coreui-icons.min.css",
        "./assets/css/flag-icon.min.css",
        "./assets/css/font-awesome.min.css",
        "./assets/css/simple-line-icons.min.css",
        "./assets/css/style.min.css",
        "./assets/css/pace.min.css",
    ])
        .pipe(cssmin())
        .pipe(concat('jp.min.css'))
        .pipe(gulp.dest('./wwwroot/css/'));
});
