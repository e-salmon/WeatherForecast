const autoRefreshInterval = 30;

/* NOTE: Using plain JS as JQuery use isn't necessary for what's being done. */
document.addEventListener("DOMContentLoaded", function() {
    if (document.getElementById("forecastData")) {
        var vForecast = new Vue({
            el: "#forecastData",
            created: function() {
                this.updateForecast();
                this.startPolling();
            },
            beforeDestroy: function() {
                clearInterval(this.polling);
            },
            methods: {
                startPolling: function() {
                    let self = this;
                    this.polling = setInterval(function() {
                        self.countdown--;
                        if (self.countdown <= 0) {
                            self.updateForecast();
                        } else {
                            self.progress = (100 - (self.countdown * 100 / autoRefreshInterval)).toString() + "%";
                        }
                    }, 1000);
                },
                updateForecast: async function() {
                    this.isRefreshing = true;
                    try {
                        const response = await fetch("/forecast-update");
                        if (response.ok) {
                            const content = await response.json();
                            console.debug(content);
                            this.weatherData = content;
                            this.hasData = true;
                            this.countdown = autoRefreshInterval;
                            this.progress = "0%";
                        }
                    } finally {
                        this.isRefreshing = false;
                    }
                }
            },
            data: {
                polling: undefined,
                countdown: 0,
                progress: "0%",
                isRefreshing: false,
                hasData: false,
                weatherData: undefined
            }
        });
    }
});

