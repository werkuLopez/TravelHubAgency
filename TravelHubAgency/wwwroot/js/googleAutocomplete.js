async function initAutocomplete() {
    var input = $("#inputDestinoAutocomplete")[0];
    var autocomplete = await new google.maps.places.Autocomplete(input);

    const btnSearch = $("#btnSearch");
    btnSearch.on("click", function () {
        var place = autocomplete.getPlace();
        getGoogleMapsIframes(place);
    });
}

async function getGoogleMapsIframes(destino) {
    console.log(destino);
}