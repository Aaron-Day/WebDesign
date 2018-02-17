function display() {


    var fname = document.getElementById("firstname").value;
    var lname = document.getElementById("lastname").value;
    var age = document.getElementById("age").value;

    var summary = fname + " " + lname + " is " + age + " years old.";

    if (fname === "" || lname === "" || age === "") {
        alert("Fill out name and age completely");
    } else {
        document.getElementById("summary").innerText = summary;
    }

    return false;
}