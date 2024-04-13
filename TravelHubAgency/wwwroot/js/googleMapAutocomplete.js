function initAutocomplete() {
    var input = $("#inputDestinoAutocomplete")[0];
    var autocomplete = new google.maps.places.Autocomplete(input);

    const btnSearch = $("#btnSearch");
    btnSearch.on("click", function () {
        var place = autocomplete.getPlace();
        getGoogleMapsIframes(place);
    });
}

function getGoogleMapsIframes(destino) {
    console.log(destino.name);
}