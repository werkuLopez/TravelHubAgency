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

    $(function () {
        $.ajax({
            url: "/Destinos/Index?destinosearched=" + destino.name,
            method: "POST",
            success: function () {
                console.log("destino buscado: ", destino.name);
            };
        });
    });
}
