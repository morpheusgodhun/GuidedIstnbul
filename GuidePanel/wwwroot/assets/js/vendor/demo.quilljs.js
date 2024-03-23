var quill = new Quill("#snow-editor", {
    theme: "snow",
    modules: {
        toolbar: [
            [{ font: [] }, { size: [] }],
            ["bold", "italic", "underline", "strike"],
            [{ color: [] }, { background: [] }],
            [{ script: "super" }, { script: "sub" }],
            [{ header: [!1, 1, 2, 3, 4, 5, 6] }, "blockquote", "code-block"],
            [{ list: "ordered" }, { list: "bullet" }, { indent: "-1" }, { indent: "+1" }],
            ["direction", { align: [] }],
            ["link", "image", "video"],
            ["clean"],
        ],
    },
})
var quill2 = new Quill("#snow-editor2", {
    theme: "snow",
    modules: {
        toolbar: [
            [{ font: [] }, { size: [] }],
            ["bold", "italic", "underline", "strike"],
            [{ color: [] }, { background: [] }],
            [{ script: "super" }, { script: "sub" }],
            [{ header: [!1, 1, 2, 3, 4, 5, 6] }, "blockquote", "code-block"],
            [{ list: "ordered" }, { list: "bullet" }, { indent: "-1" }, { indent: "+1" }],
            ["direction", { align: [] }],
            ["link", "image", "video"],
            ["clean"],
        ],
    },
})
var quill3 = new Quill("#snow-editor3", {
    theme: "snow",
    modules: {
        toolbar: [
            [{ font: [] }, { size: [] }],
            ["bold", "italic", "underline", "strike"],
            [{ color: [] }, { background: [] }],
            [{ script: "super" }, { script: "sub" }],
            [{ header: [!1, 1, 2, 3, 4, 5, 6] }, "blockquote", "code-block"],
            [{ list: "ordered" }, { list: "bullet" }, { indent: "-1" }, { indent: "+1" }],
            ["direction", { align: [] }],
            ["link", "image", "video"],
            ["clean"],
        ],
    },
}),
quill = new Quill("#bubble-editor", { theme: "bubble" });
quill2 = new Quill("#bubble-editor", { theme: "bubble" });
quill3 = new Quill("#bubble-editor", { theme: "bubble" });


function openDiv(divID) {
    var altBolum = document.getElementById(divID);
    altBolum.style.display = "block";

}
function closeDiv(divID) {

    var altBolum = document.getElementById(divID);
    altBolum.style.display = "none";
}

function changeDiv(openDivID, closeDivID){
    var openDiv = document.getElementById(openDivID);
    var closeDiv = document.getElementById(closeDivID);
    openDiv.style.display = "block";
    closeDiv.style.display = "none";

}
function newOffer(value,divID) {
    var altBolum = document.getElementById(divID);
    if (value == 1) {
        altBolum.style.display = "block";
    }else{
        altBolum.style.display = "none";
    }   
    

}