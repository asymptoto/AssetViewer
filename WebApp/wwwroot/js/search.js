function search(page = null) {
    var perPageSelect = document.getElementById('perPageSelect');
    var searchBar = document.getElementById('searchBar');
    if (searchBar.value != "") {
        query = '/search?query=' + escape(searchBar.value);
        if (perPageSelect != null) query = query + '&perpage=' + perPageSelect.value;
        if (page != null) query = query + '&page=' + page;
        console.log(query);
        window.location = query;
    } else {
        searchBar.style.backgroundColor = "#ffcccc";
    }
}

function enterPressed(e, input) {
    var code = (e.keyCode ? e.keyCode : e.which);
    if (code == 13) {
        search();
        return false;
    }
}