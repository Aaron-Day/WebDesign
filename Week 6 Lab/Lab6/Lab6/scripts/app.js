﻿function display() {

    alert("displaying");

    var fname = document.getElementById("firstname").value;
    var lname = document.getElementById("lastname").value;
    var age = document.getElementById("age").value;

    var summary = fname + " " + lname + " is " + age + " years old.";

    document.getElementById("summary").innerText = summary;

    return false;
}