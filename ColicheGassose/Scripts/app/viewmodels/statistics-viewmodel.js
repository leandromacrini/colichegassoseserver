function StatisticsViewModel() {
    var self = this;

    self.SymptomDurations = [
	'5 secondi',
	'10 secondi',
	'15 secondi',
	'30 secondi',
	'1 minuto',
	'Alcuni minuti'
    ];

    self.SymptomIntensity = [
        "Leggero",
        "Basso",
        "Medio",
        "Intenso",
        "Fortissimo"
    ];

    self.RemedyNames = [
	    "Terapia posizionale",
	    "Massaggio",
	    "Musica dolce",
	    "Movimento",
	    "Probiotici",
	    "Avviso personale"
    ];

    self.pageTitle = "Pagina di visualizzazione statistiche <small>Coliche Gassose Server</small>";

    self.updating = ko.observable(false);

    //USERS
    self.todayAccesses = ko.observable(0);
    self.monthAccesses = ko.observable(0);
    self.totalUsers = ko.observable(0);

    //SINTOMI
    self.weeklyPianti = ko.observable(0);
    self.weeklyPiantiIntensitaMedia = ko.observable(0);
    self.weeklyPiantiDurataMedia = ko.observable(0);

    self.weeklyRigurgiti = ko.observable(0);
    self.weeklyRigurgitiIntensitaMedia = ko.observable(0);
    self.weeklyRigurgitiDurataMedia = ko.observable(0);

    self.weeklyAgitazioni = ko.observable(0);
    self.weeklyAgitazioniIntensitaMedia = ko.observable(0);
    self.weeklyAgitazioniDurataMedia = ko.observable(0);

    self.weeklyPiantiVLS = ko.observable(0);
    self.weeklyPiantiIntensitaMediaVLS = ko.observable(0);
    self.weeklyPiantiDurataMediaVLS = ko.observable(0);

    self.weeklyRigurgitiVLS = ko.observable(0);
    self.weeklyRigurgitiIntensitaMediaVLS = ko.observable(0);
    self.weeklyRigurgitiDurataMediaVLS = ko.observable(0);

    self.weeklyAgitazioniVLS = ko.observable(0);
    self.weeklyAgitazioniIntensitaMediaVLS = ko.observable(0);
    self.weeklyAgitazioniDurataMediaVLS = ko.observable(0);

    // RIMEDI
    self.totalRimedi = ko.observable(0);
    self.totalRimediVLS = ko.observable(0);

    //APP
    self.deviceTypes = ko.observable(0);

    self.currentPage = ko.observable(null);
    self.setCurrentPage = function (page) {
        //set visible panel
        self.currentPage(page);

        //call backend and update statistics
        switch (page) {
            case StatisticsViewModelPages.Sintomi:
                self.updating(true);
                $.ajax({
                    url: siteRoot + 'Statistics/GetSintomiStatistics',
                    type: "POST",
                    success: function (response) {
                        self.updating(false);

                        self.weeklyPianti(response.weeklyPianti);
                        self.weeklyPiantiIntensitaMedia(response.weeklyPiantiIntensitaMedia);
                        self.weeklyPiantiDurataMedia(response.weeklyPiantiDurataMedia);

                        self.weeklyRigurgiti(response.weeklyRigurgiti);
                        self.weeklyRigurgitiIntensitaMedia(response.weeklyRigurgitiIntensitaMedia);
                        self.weeklyRigurgitiDurataMedia(response.weeklyRigurgitiDurataMedia);

                        self.weeklyAgitazioni(response.weeklyAgitazioni);
                        self.weeklyAgitazioniIntensitaMedia(response.weeklyAgitazioniIntensitaMedia);
                        self.weeklyAgitazioniDurataMedia(response.weeklyAgitazioniDurataMedia);

                        self.weeklyPiantiVLS(response.weeklyPiantiVLS);
                        self.weeklyPiantiIntensitaMediaVLS(response.weeklyPiantiIntensitaMediaVLS);
                        self.weeklyPiantiDurataMediaVLS(response.weeklyPiantiDurataMediaVLS);

                        self.weeklyRigurgitiVLS(response.weeklyRigurgitiVLS);
                        self.weeklyRigurgitiIntensitaMediaVLS(response.weeklyRigurgitiIntensitaMediaVLS);
                        self.weeklyRigurgitiDurataMediaVLS(response.weeklyRigurgitiDurataMediaVLS);

                        self.weeklyAgitazioniVLS(response.weeklyAgitazioniVLS);
                        self.weeklyAgitazioniIntensitaMediaVLS(response.weeklyAgitazioniIntensitaMediaVLS);
                        self.weeklyAgitazioniDurataMediaVLS(response.weeklyAgitazioniDurataMediaVLS);

                        var ctx;

                        if (self.chartSintomi) self.chartSintomi.destroy();
                        ctx = $("#chart-sintomi").get(0).getContext("2d");
                        self.chartSintomi = new Chart(ctx).Doughnut(response.symptomsCounts);

                        if (self.chartSintomiVLS) self.chartSintomiVLS.destroy();
                        ctx = $("#chart-sintomi-vls").get(0).getContext("2d");
                        self.chartSintomiVLS = new Chart(ctx).Doughnut(response.symptomsCountsVLS);
                    }
                });
                break;
            case StatisticsViewModelPages.Rimedi:
                self.updating(true);
                $.ajax({
                    url: siteRoot + 'Statistics/GetRimediStatistics',
                    type: "POST",
                    success: function (response) {
                        self.updating(false);

                        self.totalRimedi(response.totalRimedi);
                        self.totalRimediVLS(response.totalRimediVLS);

                        var ctx;

                        if (self.chartRimedi) self.chartRimedi.destroy();
                        ctx = $("#chart-rimedi").get(0).getContext("2d");
                        self.chartRimedi = new Chart(ctx).Line({
                            labels: response.rimedi.labels,
                            datasets: [{
                                label: "Accessi unici",
                                fillColor: "rgba(108, 134, 245, 0.5)",
                                strokeColor: "rgba(108, 134, 245, 1)",
                                pointColor: "rgba(108, 134, 245, 1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(108, 134, 245, 1)",
                                data: response.rimedi.data
                            }]
                        });

                        if (self.chartRimediVLS) self.chartRimediVLS.destroy();
                        ctx = $("#chart-rimedi-vls").get(0).getContext("2d");
                        self.chartRimediVLS = new Chart(ctx).Line({
                            labels: response.rimediVLS.labels,
                            datasets: [
                                {
                                    label: "Utenti registrati",
                                    fillColor: "rgba(245, 134, 108, 0.5)",
                                    strokeColor: "rgba(245, 134, 108, 1)",
                                    pointColor: "rgba(245, 134, 108, 1)",
                                    pointStrokeColor: "#fff",
                                    pointHighlightFill: "rgba(245, 134, 108, 0.75)",
                                    pointHighlightStroke: "rgba(245, 134, 108, 1)",
                                    data: response.rimediVLS.data
                                }
                            ]
                        });
                    }
                });
                break;
            case StatisticsViewModelPages.Appuntamenti:
                break;
            case StatisticsViewModelPages.Utenti:
                self.updating(true);
                $.ajax({
                    url: siteRoot + 'Statistics/GetUtentiStatistics',
                    type: "POST",
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
                                label : "Accessi unici",
                                fillColor: "rgba(245, 134, 108, 0.5)",
                                strokeColor: "rgba(245, 134, 108, 1)",
                                pointColor: "rgba(245, 134, 108, 1)",
                                pointStrokeColor : "#fff",
                                pointHighlightFill : "#fff",
                                pointHighlightStroke: "rgba(245, 134, 108, 1)",
                                data : response.accesses.data
                            }]
                        });

                        if (self.chartUtentiRegistrazioni) self.chartUtentiRegistrazioni.destroy();
                        ctx = $("#chart-utenti-registrazioni").get(0).getContext("2d");
                        self.chartUtentiRegistrazioni = new Chart(ctx).Bar({
                            labels: response.newusers.labels,
                            datasets: [
                                {
                                    label: "Utenti registrati",
                                    fillColor: "rgba(245, 134, 108, 0.5)",
                                    strokeColor: "rgba(245, 134, 108, 1)",
                                    highlightFill: "rgba(245, 134, 108, 0.75)",
                                    highlightStroke: "rgba(245, 134, 108, 1)",
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
                                fillColor: "rgba(245, 134, 108, 0.5)",
                                strokeColor: "rgba(245, 134, 108, 1)",
                                pointColor: "rgba(245, 134, 108, 1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(245, 134, 108, 1)",
                                data: response.totals.data
                            }]
                        });
                    }
                });
                break;
            case StatisticsViewModelPages.App:
                self.updating(true);
                $.ajax({
                    url: siteRoot + 'Statistics/GetAppStatistics',
                    type: "POST",
                    success: function (response) {
                        self.updating(false);

                        var ctx;

                        self.deviceTypes(response.deviceTypes);

                        if (self.chartAppVersion) self.chartAppVersion.destroy();
                        ctx = $("#chart-app-version").get(0).getContext("2d");
                        self.chartAppVersion = new Chart(ctx).Doughnut(response.deviceTypes);
                    }
                });
                break;
        }
    };
    
    //init charts
    Chart.defaults.global.responsive = true;
    Chart.defaults.global.scaleBeginAtZero = true;
}

var StatisticsViewModelPages = {
    Sintomi: 0,
    Rimedi: 1,
    Appuntamenti: 2,
    Utenti: 3,
    App: 4
};