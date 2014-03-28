var lastSearch;

function filter(containerClass, searchBox) {
    var searchText = $(searchBox).val();

    if (searchText == lastSearch) {
        return;
    }

    lastSearch = searchText;

    $.ajax({
        url: "/search/" + searchText,
        success: function(htmlData) {
            $("#cardrow").html(htmlData);
        }});
}
