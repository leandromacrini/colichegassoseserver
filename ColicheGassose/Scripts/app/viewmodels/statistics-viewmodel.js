function StatisticsViewModel() {
    var self = this;

    self.pageTitle = "Pagina di visualizzazione statistiche <small>Coliche Gassose Server</small>";

    self.pages = {
        Sintomi: 0,
        Rimedi: 1,
        Appuntamenti: 2,
        Utenti: 3,
        App: 4
    };

    self.updating = ko.observable(false);

    self.todayAccesses = ko.observable(0);
    self.monthAccesses = ko.observable(0);
    self.totalUsers = ko.observable(0);

    self.currentPage = ko.observable(null);
    self.setCurrentPage = function (page) {
        //set visible panel
        self.currentPage(page);

        //call backend and update statistics
        switch (page) {
            case self.pages.Sintomi:

                break;
            case self.pages.Rimedi:
                break;
            case self.pages.Appuntamenti:
                break;
            case self.pages.Utenti:
                self.updating(true);
                $.ajax({
                    url: "../../api/GetUtentiStatistics",
                    type: "GET",
                    success: function (response) {
                        self.updating(false);

                        self.todayAccesses(response.todayAccesses);
                        self.monthAccesses(response.monthAccesses);
                        self.totalUsers(response.totalUsers);

                        var ctx;

                        if (self.chartUtentiUtilizzo) self.chartUtentiUtilizzo.destroy();
                        ctx = $("#chart-utenti-utilizzo").get(0).getContext("2d");
                        self.chartUtentiUtilizzo = new Chart(ctx).Line({
                            labels : response.accesses.labels,
                            datasets : [{
                                label : "Utenti totali",
                                fillColor : "rgba(151,187,205,0.2)",
                                strokeColor : "rgba(151,187,205,1)",
                                pointColor : "rgba(151,187,205,1)",
                                pointStrokeColor : "#fff",
                                pointHighlightFill : "#fff",
                                pointHighlightStroke : "rgba(151,187,205,1)",
                                data : response.accesses.data
                            }]
                        });

                        if (self.chartUtentiRegistrazioni) self.chartUtentiRegistrazioni.destroy();
                        ctx = $("#chart-utenti-registrazioni").get(0).getContext("2d");
                        self.chartUtentiRegistrazioni = new Chart(ctx).Bar({
                            labels: response.newusers.labels,
                            datasets: [
                                {
                                    label: "My First dataset",
                                    fillColor: "rgba(220,220,220,0.5)",
                                    strokeColor: "rgba(220,220,220,0.8)",
                                    highlightFill: "rgba(220,220,220,0.75)",
                                    highlightStroke: "rgba(220,220,220,1)",
                                    data: response.newusers.data
                                }
                            ]
                        });

                        if (self.chartUtentiTotali) self.chartUtentiTotali.destroy();
                        ctx = $("#chart-utenti-totali").get(0).getContext("2d");
                        self.chartUtentiTotali = new Chart(ctx).Line({
                            labels: response.totals.labels,
                            datasets: [{
                                label: "Utenti totali",
                                fillColor: "rgba(151,187,205,0.2)",
                                strokeColor: "rgba(151,187,205,1)",
                                pointColor: "rgba(151,187,205,1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(151,187,205,1)",
                                data: response.totals.data
                            }]
                        });
                    }
                });
                break;
            case self.pages.App:
                break;
        }
    };
    
    //init charts
    Chart.defaults.global.responsive = true;
    Chart.defaults.global.scaleBeginAtZero = true;

    self.setCurrentPage(self.pages.Utenti);
}