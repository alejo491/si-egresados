$(window).load(function () {
    var postid = $("#idpost").text();
    setInterval("actualizar_num_likes('"+postid+"')", 10000);
});

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

function ClickLike(postid, tipo, like) {   
    if (tipo == 0) {
        $.ajax({
            type: "POST",
            url: "/Like/Create", 
            traditional: true,
            data: {post:postid},
            async:false,
            success : function(data){
                $("#LinknoLike").unbind("click");
                $("#LinknoLike").bind("click", function() {ClickLike(postid, '1', data)});
            }
        });
        actualizar_num_likes(postid);
        $("#LinkLike").css("display", "none");
        $("#LinknoLike").css("display", "block");       
    }
    if (tipo == 1) {
        $.ajax({
            type: "POST",
            url: "/Like/DeleteConfirmed", 
            traditional: true,
            data: {post:postid, id: like},  
            async:false,
        });
        actualizar_num_likes(postid);
        $("#LinkLike").css("display", "block");
        $("#LinknoLike").css("display", "none");         
    }
    
    
}   

function actualizar_num_likes(postid){
        $.ajax({
            type: "POST",
            url: "/Like/get_likes", 
            traditional: true,
            data: {id:postid},
            success : function(data){
                $("#NumLike").html("&nbsp&nbsp&nbsp" + data + " Usuarios les Gusta");  
            }
        });
}
