var img;

window.ondragover = function (e) { e.preventDefault() }
window.ondrop = function (e) { e.preventDefault(); upload(e.dataTransfer.files[0]); }
function upload(file) {

    if (!file || !file.type.match(/image.*/)) return;

    document.body.className = "uploading";
    var fd = new FormData(); // I wrote about it: https://hacks.mozilla.org/2011/01/how-to-develop-a-html5-image-uploader/
    fd.append("image", file); // Append the file
    var xhr = new XMLHttpRequest(); // Create the XHR (Cross-Domain XHR FTW!!!) Thank you sooooo much imgur.com
    xhr.open("POST", "https://api.imgur.com/3/image.json"); // Boooom!
    var elem = document.createElement("img");
    xhr.onload = function () {
        // Big win!
        document.querySelector("#link").href = JSON.parse(xhr.responseText).data.link;
        id = document.querySelector("#link").href;
        var img = document.getElementById(id);
        elem.src = document.querySelector("#link").href;
        var imgObj = new Image();
        imgObj.src = id;
        elem.setAttribute("height", 100);
        elem.setAttribute("width", 100);
        elem.setAttribute("id", document.querySelector("#link").href);
        document.getElementById("addDiv").appendChild(elem);
        document.body.className = "uploaded";
    }
    xhr.setRequestHeader('Authorization', 'Client-ID 28aaa2e823b03b1'); // Get your own key http://api.imgur.com/
    xhr.send(fd);
    return id;
}



function getMousePos(mouseEvent) {
    var result = {
        x: 0,
        y: 0
    };

    if (!mouseEvent) {
        mouseEvent = window.event;
    }

    if (mouseEvent.pageX || mouseEvent.pageY) {
        result.x = mouseEvent.pageX;
        result.y = mouseEvent.pageY;
    }
    else if (mouseEvent.clientX || mouseEvent.clientY) {
        result.x = mouseEvent.clientX + document.body.scrollLeft +
        document.documentElement.scrollLeft;
        result.y = mouseEvent.clientY + document.body.scrollTop +
        document.documentElement.scrollTop;
    }

    if (mouseEvent.target) {
        var offEl = mouseEvent.target;
        var offX = 0;
        var offY = 0;

        if (typeof (offEl.offsetParent) != "undefined") {
            while (offEl) {
                offX += offEl.offsetLeft;
                offY += offEl.offsetTop;

                offEl = offEl.offsetParent;
            }
        }
        else {
            offX = offEl.x;
            offY = offEl.y;
        }

        result.x -= offX;
        result.y -= offY;
    }

    return result;
};

