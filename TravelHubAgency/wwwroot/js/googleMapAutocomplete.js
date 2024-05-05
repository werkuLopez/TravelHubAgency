
var autocomplete;

$(function () {

    initAutocomplete();

    $(document).on("click", "#btnSearch", function () {
        if (autocomplete) {
            var place = autocomplete.getPlace();
            if (place.geometry) {
                getGoogleMapsIframes(place);
            } else {
                console.error("Lugar no válido");
            }
        } else {
            console.error("Autocompletado no inicializado");
        }
    });
});

function initAutocomplete() {
    var input = $(".inputDestinoAutocomplete")[0];
    autocomplete = new google.maps.places.Autocomplete(input);

}

function getGoogleMapsIframes(destino) {
    console.log(destino);
    //$("#contentDestinos").load("/Destinos/_Destinos?destinosearched=" + destino.name);
    $.ajax({
        url: "/Destinos/Index?destinosearched=" + destino.name,
        method: "GET",
        success: function () {
            console.log("destino buscado: ", destino.name);
            //window.location.reload();
        },
        error: function (status, error) {
            console.log(status);
            console.error(error);
        }
    });
}
