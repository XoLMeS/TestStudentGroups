// Get the modal
var modal = document.getElementById('groupModal');
var modal2 = document.getElementById('studModal');
var modal3 = document.getElementById('entryModal');

window.onclick = function (event) {
    if (event.target == modal || event.target == modal2 || event.target == modal3) {
        $(".modal").css("display", "none");
    }
}


$("#studBtn").click(function () {
    $("#studModal").css("display","block")
});

$("#groupBtn").click(function () {
    $("#groupModal").css("display", "block")
});

$("#entryBtn").click(function () {
    $("#entryModal").css("display", "block")
});

$(".close").click(function () {
    $(".modal").css("display", "none");
});



/* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
function selectToggle(str) {
    if (str == "group") {
        document.getElementById("groupSelect").classList.toggle("show");
    }
    else {
        document.getElementById("studentSelect").classList.toggle("show");
    }
    filter(str);
}


function filter(str) {
    var input, div;
    if (str == "group") {
        input = document.getElementById("groupInput");
        div = document.getElementById("groupSelect");
    }
    else {
        input = document.getElementById("studentInput");
        div = document.getElementById("studentSelect");
    }

    filterFunction(input, div);
}


function filterFunction(input, div) {
    var filter, a, i;
    filter = input.value.toUpperCase();
    a = div.getElementsByTagName("a");
    var k = 0;
    for (i = 0; i < a.length; i++) {
        if (a[i].innerHTML.toUpperCase().indexOf(filter) > -1 && k<10) {
            a[i].style.display = "";
            k++;
        } else {
            a[i].style.display = "none";
        }
    }
}

function chooseGroup(item) {
    $("#groupSelectBtn").html(item.target.childNodes[0].textContent);
}

function chooseStudent(item) {
    $("#studentSelectBtn").html(item.target.childNodes[0].textContent);
}