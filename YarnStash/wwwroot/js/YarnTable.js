function ColumnSortWords(n) {
    flipArrow(n);
    sortRowsWords(n);
}

function ColumnSortInt(n) {
    flipArrow(n);
    sortRowsInt(n);
}

//TO DO: optimize sorts

function sortRowsWords(n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir;
    var switchCount = 0;


    table = document.getElementById("yarnTable");
    switching = true;

    //sets sorting direction to ascending

    dir = "asc";

    //loop continues until no switching was done

    while (switching) {
        switching = false;
        rows = table.rows;

        //loop through table rows excluding first

        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;

            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];

            //check if the 2 lines should switch

            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
            else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
        }

        if (shouldSwitch) {
            //switch marked switch

            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchCount++;
        }
        else {
            if (switchCount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}


function sortRowsInt(n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir;
    var switchCount = 0;


    table = document.getElementById("yarnTable");
    switching = true;

    //sets sorting direction to ascending

    dir = "asc";

    //loop continues until no switching was done

    while (switching) {
        switching = false;
        rows = table.rows;

        //loop through table rows excluding first

        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;

            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];

            //check if the 2 lines should switch

            if (dir == "asc") {
                if (parseInt(x.innerText, 10) > parseInt(y.innerText, 10)) {
                    shouldSwitch = true;
                    break;
                }
            }
            else if (dir == "desc") {
                if (parseInt(x.innerText, 10) < parseInt(y.innerText, 10)) {
                    shouldSwitch = true;
                    break;
                }
            }
        }

        if (shouldSwitch) {
            //switch marked switch

            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchCount++;
        }
        else {
            if (switchCount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}

function flipArrow(n) {
    var table = document.getElementById("yarnTable");
    var headers = table.getElementsByTagName("th");


    if (headers[n].className.match(/(?:^|\s)sorting_asc(?!\S)/)) {
        headers[n].className = "sorting_desc";
    }

    else if (headers[n].className.match(/(?:^|\s)sortable(?!\S)/)) {
        headers[n].className = "sorting_asc";
    }

    else if (headers[n].className.match(/(?:^|\s)sorting_desc(?!\S)/)) {
        headers[n].className = "sorting_asc";
    }

    for (i = 0; i < (headers.length - 1); i++) {
        if (i != n && headers[i].className.match(/(?:^|\s)sort/)) {
            headers[i].className = "sortable";
        }
    }
}

