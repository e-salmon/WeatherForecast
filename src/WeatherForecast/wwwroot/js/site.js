const autoRefreshInterval = 30;
let countdown = autoRefreshInterval;
$(function() {
    if ($("#forecastContent").length) {
        updateForecast();
        setInterval(
            function() {
                countdown--;
                if (countdown <= 0) {
                    updateForecast();
                } else {
                    $("#autoRefresh").css("width", (100 - (countdown * 100 / autoRefreshInterval)).toString() + "%");
                }
            },
            1000);
    }
    $("#btnRefresh").on("click", function() { updateForecast(); });
});

function updateForecast() {
    $("#btnRefresh i").addClass("fa-spin");
    $.get("/forecast-update",
            function(data) {
                $("#forecastContent").html(data);
                countdown = autoRefreshInterval;
        })
        .fail(function() {
            // Basic logging in case something went wrong.
            console.log(arguments);
        })
        .always(function() {
            $("#btnRefresh i").removeClass("fa-spin");
        });
}