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

async function updateForecast() {
    $("#btnRefresh i").addClass("fa-spin");
    const response = await fetch("/forecast-update");
    if (response.ok) {
        const content = await response.text();
        $("#forecastContent").html(content);
        countdown = autoRefreshInterval;
    } else {
        console.log(response);
    }

    $("#btnRefresh i").removeClass("fa-spin");
}