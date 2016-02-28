angular.module('pw.canvas-painter').controller('MainController', function ($scope) {
    this.undo = function () {
        this.version--;
    };
    this.clear = function () {
        this.version = 0;
    };

    function downloadCanvas(link, canvasId, filename) {
        link.href = document.getElementById(canvasId).toDataURL();
        link.download = filename;
    }
    document.getElementById('download').addEventListener('click', function () {
        downloadCanvas(this, 'pwCanvasMain', 'image.png');
    }, false);

    this.newPic = function () {
    };

});