
//打开或关闭子窗口
function ShowAndHide(tag,location) {
    if (tag == "show") {
        document.getElementsByClassName(location)[0].style.display = "block";
        fade.style.display = 'block';
    }
    if (tag == "hide") {
        document.getElementsByClassName(location)[0].style.display = "none";
        fade.style.display = 'none';
    }
}



//登录框里的提示文字*@
//$(".login_UserInput").focus(function () {
//    $(".user_label")[0].className += " Signln_label";
//    //if ($("#UserName").val() != "") {
//    //    (function () {
//    //        $(".user_label").css("display", "none");
//    //    }())
//    //}
//});


//$(".login_UserInput").blur(function () {
//    $(".user_label")[0].className = "user_label";
//})
//$(".login_PwdInput").focus(function () {
//    $(".user_label")[0].className += " Signln_label";
//}); 
//$(".login_PwdInput").blur(function () {
//    $(".user_label")[0].className = "user_label";
//})