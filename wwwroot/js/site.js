
function validate(); {
    var name = document.getElementById("name").value;
    var email = document.getElementById("email").value;
    var message = document.getElementById("message").value;
    var error_message = document.getElementById("error_message");
    var text

    error_message.style.padding = "10px";

    var text;
    if (name.length < 5) {
        text = "Please Enter valid Name";
        error_message.innerHTML = text;
        return false;
    }
}
if (email.indexOf("@") == -1 || email.length < 6) {
    text = "Please Enter valid Email";
    error_message.innerHTML = text;
    return false;
}
if (message.length <= 100) {
    text = "Please Enter More Than 100 Characters";
    error_message.innerHTML = text;
    return false;
}
{
    alert("Form Submitted Successfully!");
    return true;
}

function readURL(input) {
    var reader = new FileReader();

    reader.onload = function (e) {
        document.getElementById("Img").setAttribute("src", e.target.result);
    };
    reader.readAsDataURL(input.files[0]);


}
