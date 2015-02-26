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

    self.currentPage = ko.observable(self.pages.Utenti);
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
                        if (self.chartUtentiUtilizzo) self.chartUtentiUtilizzo.destroy();
                        var ctx = $("#chart-utenti-utilizzo").get(0).getContext("2d");
                        self.chartUtentiUtilizzo = new Chart(ctx).Line(response);
                    }
                });
                break;
            case self.pages.App:
                break;
        }
    };
    
    //init charts
    Chart.defaults.global.responsive = true;
}