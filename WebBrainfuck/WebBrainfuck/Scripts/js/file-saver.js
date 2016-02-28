function toggle1() {
    var ele = document.getElementById("toggleText1");
    var text = document.getElementById("displayText");
    if(ele.style.display == "block") {
        ele.style.display = "none";
        text.innerHTML = "Create new file";
    }
    else {
        ele.style.display = "block";
        text.innerHTML = "Don't show this message";
    }
}

function toggle2() {
    var ele = document.getElementById("toggleText2");
    var text = document.getElementById("displayText");
    if (ele.style.display == "block") {
        ele.style.display = "none";
        text.innerHTML = "Edit current file";
    }
    else {
        ele.style.display = "block";
        text.innerHTML = "Don't show this message";
    }
}

    function saveTextAsFile() {
        var textToWrite = document.getElementById("inputTextToSave").value;
        var textFileAsBlob = new Blob([textToWrite], { type: 'text/plain' });
        var fileNameToSaveAs = document.getElementById("inputFileNameToSaveAs").value;

        var downloadLink = document.createElement("a");
        downloadLink.download = fileNameToSaveAs;
        downloadLink.innerHTML = "Download File";
        if (window.webkitURL != null) {
            downloadLink.href = window.webkitURL.createObjectURL(textFileAsBlob);
        }
        else {
            downloadLink.href = window.URL.createObjectURL(textFileAsBlob);
            downloadLink.onclick = destroyClickedElement;
            downloadLink.style.display = "none";
            document.body.appendChild(downloadLink);
        }

        downloadLink.click();
    }

function destroyClickedElement(event) {
    document.body.removeChild(event.target);
}
