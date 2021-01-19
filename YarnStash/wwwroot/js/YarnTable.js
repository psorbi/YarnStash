
function ColumnSortWords(n) {
    sortByColumnText(n);
    flipArrow(n);
}

function ColumnSortInt(n) {
    sortByColumnInt(n);
    flipArrow(n);
}


function sortByColumnText(n) {
    var rows, switching, i, x, y, shouldSwitch;
    var table = document.getElementsByClassName("sortableTable")[0];
    var headers = table.getElementsByTagName("th");

    switching = true;

    //if class is sorting_asc sort direction is desc

    if (headers[n].className.match(/(?:^|\s)sorting_asc(?!\S)/)) {

        while (switching) {
            switching = false;
            rows = table.rows;

            for (i = 1; i < (rows.length - 1); i++) {
                shouldSwitch = false;
                x = rows[i].getElementsByTagName("td")[n];
                y = rows[i + 1].getElementsByTagName("td")[n];

                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
            if (shouldSwitch) {
                rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                switching = true;
            }
        }

    }

    // if class is sorting_desc or sortable sort direction is asc

    else if (headers[n].className.match(/(?:^|\s)sorting_desc(?!\S)/) ||
        headers[n].className.match(/(?:^|\s)sortable(?!\S)/)) {

        while (switching) {
            switching = false;
            rows = table.rows;

            for (i = 1; i < (rows.length - 1); i++) {
                shouldSwitch = false;
                x = rows[i].getElementsByTagName("td")[n];
                y = rows[i + 1].getElementsByTagName("td")[n];

                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    shouldSwitch = true;
                    break;
                }
            }
            if (shouldSwitch) {
                rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                switching = true;
            }
        }
    }
}


function sortByColumnInt(n) {
    var rows, switching, i, x, y, shouldSwitch;
    var table = document.getElementsByClassName("sortableTable")[0];
    var headers = table.getElementsByTagName("th");

    
    switching = true;

    //if class is sorting_asc sort direction is desc

    if (headers[n].className.match(/(?:^|\s)sorting_asc(?!\S)/)) {

        while (switching) {
            switching = false;
            rows = table.rows;

            for (i = 1; i < (rows.length - 1); i++) {
                shouldSwitch = false

                x = rows[i].getElementsByTagName("TD")[n];
                y = rows[i + 1].getElementsByTagName("TD")[n];

                if (parseInt(x.innerText, 10) < parseInt(y.innerText, 10)) {
                    shouldSwitch = true;
                    break;
                }
            }
            if (shouldSwitch) {
                rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                switching = true;
            }
        }

    }

    // if class is sorting_desc or sortable sort direction is asc

    else if (headers[n].className.match(/(?:^|\s)sorting_desc(?!\S)/) ||
        headers[n].className.match(/(?:^|\s)sortable(?!\S)/)) {

        while (switching) {
            switching = false;
            rows = table.rows;

            for (i = 1; i < (rows.length - 1); i++) {
                shouldSwitch = false

                x = rows[i].getElementsByTagName("TD")[n];
                y = rows[i + 1].getElementsByTagName("TD")[n];

                if (parseInt(x.innerText, 10) > parseInt(y.innerText, 10)) {
                    shouldSwitch = true;
                    break;
                }
            }
            if (shouldSwitch) {
                rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
                switching = true;
            }
        }
    }
}


function flipArrow(n) {
    var table = document.getElementsByClassName("sortableTable")[0];
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

