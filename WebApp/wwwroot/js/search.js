function search() {
    var searchBar = document.getElementById('searchBar');
    if (searchBar.value != "") {
        window.location = '/search?query=' + escape(searchBar.value);
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