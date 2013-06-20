function hover(idstar) {
    var div;
    for(var i = 0; i <= idstar; i++){
        div = "#star" + i;
        $(div).attr("class", "startselected");
    }
}

function unhover() {
    for (var i = 0; i <= 5; i++) {
        div = "#star" + i;
        $(div).attr("class", "startunselected");
    }
}