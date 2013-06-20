function AutorizarPost(postid) {
    var cb = "#CBA" + postid + ":checked";
    var value = 0;
    if($(cb).val() == 'true')
    {
        value = 1;
    }
    $.ajax({
        type: "POST",
        url: "/Post/AutorizarPost", 
        traditional: true,
        data: {id:postid, value:value}, 
    });
}

function PostPrincipal(postid) {
    var cb = "#CBM" + postid + ":checked";
    var value = 0;
    if ($(cb).val() == 'true') {
        value = 1;
    }
    $.ajax({
        type: "POST",
        url: "/Post/PostPrincipal",
        traditional: true,
        data: { id: postid, value: value },        
    });
}

function CambiarEstadoPost(postid) {
    var cb = "#CBE" + postid + ":checked";
    var value = 0;
    if ($(cb).val() == 'true') {
        value = 1;
    }
    $.ajax({
        type: "POST",
        url: "/Post/CambiarEstadoPost",
        traditional: true,
        data: { id: postid, value: value },        
    });
}