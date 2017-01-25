/// <binding BeforeBuild='min' Clean='clean' ProjectOpened='watch' />
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
	sourcemaps = require('gulp-sourcemaps'),
	watch = require('gulp-watch');

var webroot = "./wwwroot/";

var paths = {
	js: webroot + "js/**/*.js",
	minJs: webroot + "js/**/*.min.js",
	css: webroot + "css/**/*.css",
	minCss: webroot + "css/**/*.min.css",
	concatJsDest: webroot + "js/site.min.js",
	concatCssDest: webroot + "css/site.min.css"
};

gulp.task("clean:js", function (cb) {
	rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
	rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
	return gulp.src([paths.js, "!" + paths.minJs], {
			base: "."
		})
		.pipe(concat(paths.concatJsDest))
		.pipe(uglify())
		.pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
	return gulp.src([webroot + 'css/*.css', '!' + webroot + 'css/*.min.css'], {
			base: '.'
		})
		//.pipe(concat('site.bootstrap.min.css'))
		.pipe(sourcemaps.init())
		.pipe(cssmin())
		.pipe(insert.transform(function (contents, file) {
			var comment = '/* local file: ' + file.path + '*/\n';
			return comment + contents;
		}))
		.pipe(rename({
			suffix: '.min'
		}))
		.pipe(sourcemaps.write('.'))
		.pipe(gulp.dest('.'));
});

gulp.task('less', function () {
	return gulp.src(webroot + 'templates/**/site.bootstrap.less')
		.pipe(sourcemaps.init())
		.pipe(less())
		.pipe(rename(function (path) {
			var filename = path.dirname.replace('/less', '').replace('\\less', '').toLowerCase();
			path.dirname = webroot + 'css';
			path.basename = filename + '.bootstrap';
			path.extname = '.css';
		}))
		.pipe(sourcemaps.write('.'))
		.pipe(gulp.dest('.'));

});

gulp.task("style:watch", ["less", "min:css"]);
gulp.task("script:watch", ["min:js"]);

gulp.task("watch", function () {
	gulp.watch(webroot + "js/*.js", ["script:watch"]);
	gulp.watch(webroot + "**/*.less", ["style:watch"]);
});

gulp.task("min", ["less", "min:js", "min:css"]);