$(document).ready(function () {
    $("#taggs").keydown(function (event) {
        if (event.which === 13 || event.keyCode === 13) {
            SearchCDByArtist();
        }
    });
    $("#sokruta").keydown(function (event) {
        if (event.which === 13 || event.keyCode === 13) {
            SearchCDByTitle();
        }
    });
})

// Funktionen skickar valda sökord till servern;
function SearchCDByTitle() {
    var word = document.getElementById("sokruta").value;
    if (word === null || word === "")
        return;
    if (word !== null && word !== "")
        document.getElementById("sokruta").value = "";
    $.get({
        url: "/CDs/SearchCDByTitle/" + word,
        success: function (svar) {
            if (svar !== null && svar !== "") {
                CreateTable(svar);
            }
        }
    });
}

function CreateTable(data) {
    if (JSON.stringify(data) === JSON.stringify("Finns EJ")) {
        alert(JSON.stringify(data));
    }       
    else {
        if ($("#tabell table").length) {
            $("#tabell table").remove();
        }
        var table = $('<table></table>');
        var th1 = "<tr><th>Nr</th><th>Titel</th><th>Artist</th><th>Kategori</th><th>År</th></tr>";
        table.append(th1);
        for (var x = 0; x < data.length; x++) {
            var col1 = $("<td></td>").text(data[x].id);
            var col2 = $("<td></td>").text(data[x].title);
            var col3 = $("<td></td>").text(data[x].artist.name);
            var col4 = $("<td></td>").text(data[x].category);
            var col5 = $("<td></td>").text(data[x].year);
            var rad = $('<tr></tr>');
            rad.append(col1);
            rad.append(col2);
            rad.append(col3);
            rad.append(col4);
            rad.append(col5);
            table.append(rad);
        }
        $("#tabell").append(table);
    }
}

function SearchCDByArtist() {
    var word = document.getElementById("taggs").value;
    if (word === null || word === "")
        return;
    if (word !== null && word !== "")
        document.getElementById("taggs").value = "";
    $.get({
        url: "/CDs/SearchCDByArtist/" + word,
        success: function (svar) {
            if (svar !== null && svar !== "") {
                CreateTable(svar);
            }
        }
    });
}