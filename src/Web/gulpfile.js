/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    less = require("gulp-less"),
    path = require("path"),
    rename = require("gulp-rename"),
    wrap = require("gulp-wrap"),
    insert = require("gulp-insert"),
    less = require('gulp-less-sourcemap');

var webroot = "./wwwroot/";

var paths = {
    js: webroot + "js/**/*.js",
    minJs: webroot + "js/**/*.min.js",
    css: webroot + "css/**/*.css",
    minCss: webroot + "css/**/*.min.css",
    concatJsDest: webroot + "js/site.min.js",
    concatCssDest: webroot + "css/site.min.css"
};

gulp.task("clean:js", function(cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function(cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function() {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function() {
    return gulp.src(['./Content/**/css/*.css', '!./Content/**/css/*.min.css'])
        //.pipe(concat('site.bootstrap.min.css'))
        .pipe(cssmin())
        .pipe(insert.transform(function(contents, file) {
            var comment = '/* local file: ' + file.path + '*/\n';
            return comment + contents;
        }))
        .pipe(rename({ dirname: '/', suffix: '.min' }))
        .pipe(gulp.dest(webroot + 'css', { base: '.' }))
});

gulp.task('less', function() {
    return gulp.src(webroot + 'templates/**/site.bootstrap.less')
        .pipe(less({
            paths: [path.join(__dirname, 'less', 'includes')],
            sourceMap: {
                // sourceMapURL: './Content/',
                sourceMapBasepath: 'maps',
                //sourceMapRootpath: '/Content', // Optional absolute or relative path to your LESS files
                sourceMapFileInline: false,

            }
        }))
        .pipe(rename(function(path) {
            var filename = path.dirname.replace('/less', '').toLowerCase();
            //path.dirname = path.dirname.replace('/less', '/css');
            path.dirname = webroot + '/css';
            path.basename = filename + '.bootstrap';
            path.extname = '.css';
        }))
        .pipe(gulp.dest(function(file) {
            return file.base;
        }));
});

gulp.task("min", ["less", "min:js", "min:css"]);