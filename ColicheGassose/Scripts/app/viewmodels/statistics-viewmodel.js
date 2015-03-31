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

    self.weeklyPiantiVSL = ko.observable(0);
    self.weeklyPiantiIntensitaMediaVSL = ko.observable(0);
    self.weeklyPiantiDurataMediaVSL = ko.observable(0);

    self.weeklyRigurgitiVSL = ko.observable(0);
    self.weeklyRigurgitiIntensitaMediaVSL = ko.observable(0);
    self.weeklyRigurgitiDurataMediaVSL = ko.observable(0);

    self.weeklyAgitazioniVSL = ko.observable(0);
    self.weeklyAgitazioniIntensitaMediaVSL = ko.observable(0);
    self.weeklyAgitazioniDurataMediaVSL = ko.observable(0);

    // RIMEDI
    self.totalRimedi = ko.observable(0);
    self.totalRimediVSL = ko.observable(0);

    // APPUNTAMENTI
    self.totalAppointments = ko.observable(0);

    //APP
    self.deviceTypes = ko.observable(0);

    self.currentPage = ko.observable(null);
    self.setCurrentPage = function (page) {
        //set visible panel
        self.currentPage(page);

        //call backend and update statistics
        switch (page) {
            case StatisticsViewModelPages.MedieVSL:
                self.updating(true);
                $.ajax({
                    url: siteRoot + 'Statistics/GetMedieStatistics',
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

                        self.weeklyPiantiVSL(response.weeklyPiantiVSL);
                        self.weeklyPiantiIntensitaMediaVSL(response.weeklyPiantiIntensitaMediaVSL);
                        self.weeklyPiantiDurataMediaVSL(response.weeklyPiantiDurataMediaVSL);

                        self.weeklyRigurgitiVSL(response.weeklyRigurgitiVSL);
                        self.weeklyRigurgitiIntensitaMediaVSL(response.weeklyRigurgitiIntensitaMediaVSL);
                        self.weeklyRigurgitiDurataMediaVSL(response.weeklyRigurgitiDurataMediaVSL);

                        self.weeklyAgitazioniVSL(response.weeklyAgitazioniVSL);
                        self.weeklyAgitazioniIntensitaMediaVSL(response.weeklyAgitazioniIntensitaMediaVSL);
                        self.weeklyAgitazioniDurataMediaVSL(response.weeklyAgitazioniDurataMediaVSL);

                        var ctx;

                        if (self.chartSintomi) self.chartSintomi.destroy();
                        ctx = $("#chart-medie").get(0).getContext("2d");
                        self.chartSintomi = new Chart(ctx).Doughnut(response.symptomsCounts);

                        if (self.chartSintomiVSL) self.chartSintomiVSL.destroy();
                        ctx = $("#chart-medie-vsl").get(0).getContext("2d");
                        self.chartSintomiVSL = new Chart(ctx).Doughnut(response.symptomsCountsVSL);
                    }
                });
                break;
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

                        self.weeklyPiantiVSL(response.weeklyPiantiVSL);
                        self.weeklyPiantiIntensitaMediaVSL(response.weeklyPiantiIntensitaMediaVSL);
                        self.weeklyPiantiDurataMediaVSL(response.weeklyPiantiDurataMediaVSL);

                        self.weeklyRigurgitiVSL(response.weeklyRigurgitiVSL);
                        self.weeklyRigurgitiIntensitaMediaVSL(response.weeklyRigurgitiIntensitaMediaVSL);
                        self.weeklyRigurgitiDurataMediaVSL(response.weeklyRigurgitiDurataMediaVSL);

                        self.weeklyAgitazioniVSL(response.weeklyAgitazioniVSL);
                        self.weeklyAgitazioniIntensitaMediaVSL(response.weeklyAgitazioniIntensitaMediaVSL);
                        self.weeklyAgitazioniDurataMediaVSL(response.weeklyAgitazioniDurataMediaVSL);

                        var ctx;

                        if (self.chartSintomi) self.chartSintomi.destroy();
                        ctx = $("#chart-sintomi").get(0).getContext("2d");
                        self.chartSintomi = new Chart(ctx).Doughnut(response.symptomsCounts);

                        if (self.chartSintomiVSL) self.chartSintomiVSL.destroy();
                        ctx = $("#chart-sintomi-vsl").get(0).getContext("2d");
                        self.chartSintomiVSL = new Chart(ctx).Doughnut(response.symptomsCountsVSL);
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
                        self.totalRimediVSL(response.totalRimediVSL);

                        //split Rimedi by type

                        var ctx;
                        /*[
	                        "Terapia posizionale",
	                        "Massaggio",
	                        "Musica dolce",
	                        "Movimento",
	                        "Probiotici",
	                        "Avviso personale"
                        ]*/

                        if (self.chartRimedi) self.chartRimedi.destroy();
                        ctx = $("#chart-rimedi").get(0).getContext("2d");
                        self.chartRimedi = new Chart(ctx).Line({
                            labels: response.rimedi.labels,
                            datasets: [{
                                label: "Terapia posizionale",
                                fillColor: "rgba(132, 245, 108, 0)",
                                strokeColor: "rgba(132, 245, 108, 1)",
                                pointColor: "rgba(132, 245, 108, 1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(132, 245, 108, 1)",
                                data: response.rimedi.data[0]
                            },{
                                label: "Massaggio",
                                strokeColor: "rgba(108, 134, 245, 1)",
                                fillColor: "rgba(108, 134, 245, 0)",
                                pointColor: "rgba(108, 134, 245, 1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(108, 134, 245, 1)",
                                data: response.rimedi.data[1]
                            },{
                                label: "Musica dolce",
                                strokeColor: "rgba(245, 108, 217, 1)",
                                fillColor: "rgba(245, 108, 217, 0)",
                                pointColor: "rgba(245, 108, 217, 1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(245, 108, 217, 1)",
                                data: response.rimedi.data[2]
                            }, {
                                label: "Movimento",
                                fillColor: "rgba(108, 245, 245, 0)",
                                strokeColor: "rgba(108, 245, 245, 1)",
                                pointColor: "rgba(108, 245, 245, 1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(108, 245, 245, 1)",
                                data: response.rimedi.data[3]
                            }, {
                                label: "Probiotici",
                                strokeColor: "rgba(255, 0, 0, 1)",
                                fillColor: "rgba(255, 0, 0, 0)",
                                pointColor: "rgba(255, 0, 0, 1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(255, 0, 0, 1)",
                                data: response.rimedi.data[4]
                            },{
                                label: "Rimedio personale",
                                strokeColor: "rgba(245, 134, 108, 1)",
                                fillColor: "rgba(245, 134, 108, 0)",
                                pointColor: "rgba(245, 134, 108, 1)",
                                pointStrokeColor: "#fff",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(245, 134, 108, 1)",
                                data: response.rimedi.data[5]
                            }]
                        });

                        $("#rimedi-legend").html(self.chartRimedi.generateLegend());

                        if (self.chartRimediVSL) self.chartRimediVSL.destroy();
                        ctx = $("#chart-rimedi-vsl").get(0).getContext("2d");
                        self.chartRimediVSL = new Chart(ctx).Line({
                            labels: response.rimediVSL.labels,
                            datasets: [
                                {
                                    label: "Rimedi VSL",
                                    fillColor: "rgba(245, 134, 108, 0.5)",
                                    strokeColor: "rgba(245, 134, 108, 1)",
                                    pointColor: "rgba(245, 134, 108, 1)",
                                    pointStrokeColor: "#fff",
                                    pointHighlightFill: "rgba(245, 134, 108, 0.75)",
                                    pointHighlightStroke: "rgba(245, 134, 108, 1)",
                                    data: response.rimediVSL.data
                                }
                            ]
                        });
                    }
                });
                break;
            case StatisticsViewModelPages.Appuntamenti:
                self.updating(false);
                $.ajax({
                    url: siteRoot + 'Statistics/GetAppuntamentiStatistics',
                    type: "POST",
                    success: function (response) {
                        self.updating(false);

                        self.totalAppointments(response.totalAppointments);

                        if (self.chartAppuntamenti) self.chartAppuntamenti.destroy();
                        var ctx = $("#chart-appuntamenti").get(0).getContext("2d");
                        self.chartAppuntamenti = new Chart(ctx).Line({
                            labels: response.appointments.labels,
                            datasets: [
                                {
                                    label: "Rimedi VSL",
                                    fillColor: "rgba(245, 134, 108, 0.5)",
                                    strokeColor: "rgba(245, 134, 108, 1)",
                                    pointColor: "rgba(245, 134, 108, 1)",
                                    pointStrokeColor: "#fff",
                                    pointHighlightFill: "rgba(245, 134, 108, 0.75)",
                                    pointHighlightStroke: "rgba(245, 134, 108, 1)",
                                    data: response.appointments.data
                                }
                            ]
                        });
                    }
                });
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

ko.components.register('intensity-meter', {
    viewModel: function (params) {
        this.value = new Array(parseInt(params.value));
    },
    template: '<!-- ko foreach: value --><img class="intensity-icon" src="/Images/icon-sintomo.png" alt="icona sintomo" /><!-- /ko -->'
});

var StatisticsViewModelPages = {
    Sintomi: 0,
    Rimedi: 1,
    Appuntamenti: 2,
    Utenti: 3,
    App: 4,
    MedieVSL : 5
};