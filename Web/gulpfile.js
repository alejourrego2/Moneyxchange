// include plug-ins
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');

var config = {
    src: ['wwwroot/js/**/*.js',
        '!wwwroot/js/**/*.min.js',
        '!wwwroot/js/jquery/bootstrap/*',
        '!wwwroot/js/jquery/dist/*',
        '!wwwroot/js/jquery-validation/dist/*',
        '!wwwroot/js/jquery-validation-unobtrusive/*']
}

gulp.task('clean', function () {
    return del(['wwwroot/js/site.min.js']);
});

gulp.task('scripts', function () {
    gulp.series('clean');

    return gulp.src(config.src)
        .pipe(uglify())
        .pipe(concat('site.min.js'))
        .pipe(gulp.dest('wwwroot/js/'));
});

gulp.task('watch', function () {
    return gulp.watch(config.src, gulp.parallel('scripts'));
});

gulp.task('default', gulp.series('scripts', function (done) {
    done();
}));