﻿function Slots() {
    var slots = [];
    var filteredSlots = [];
    var clubLimit = 9;
    var section = "New";

    var providers = ["qt", "gpi", "mgs", "pt", "ctxm", "isb"];
    var clubs = [
        { name: "apollo", label: "Club Apollo", providers: ["qt", "pp", "gpi"] }
        , { name: "bravado", label: "Club Bravado", providers: ["gpi"] }
        , { name: "massimo", label: "Club Massimo", providers: ["mgs", "gpi"] }
        , { name: "palazzo", label: "Club Palazzo", providers: ["pt", "gpi"] }
        , { name: "divino", label: "Club Divino", providers: ["bs", "ctxm", "uc8", "gpi"] }
        , { name: "gallardo", label: "Club Gallardo", providers: ["isb", "png", "gpi"] }
    ];

    return {
        get: getSlots
        , items: slots
        , addItems: addItems
        , itemsByClub: itemsByClub
        , itemsBySection: itemsBySection
        , clubLimit: clubLimit
        , clubs: clubs
        , club: {}
        , providers: providers
        , section: section
        , toggleSection: toggleSection
        , selectClub: selectClub
        , translations: translations
        , getFilterOptions: getFilterOptions
        , showGameModal: showGameModal
    }


    function send(url, method, data, success, error, complete) {

        var headers = {
            'Token': w88Mobile.User.token,
            'LanguageCode': w88Mobile.User.lang
        };

        $.ajax({
            type: method,
            url: url,
            data: data,
            headers: headers,
            success: success,
            error: error,
            complete: complete
        });

    }

    function getSlots(provider, success, error) {
        var url = "/api/games/" + provider;
        send(url, "GET", {}, success, error);
    }

    function addItems(games, provider) {
        _.forEach(games, function (game) {
            var hasItem = _.findIndex(w88Mobile.v2.Slots.items, function (item) {
                return item.Id == game.Id;
            });
            if (hasItem == -1) {
                game.providers = _.clone(game.OtherProvider);
                if (!_.isUndefined(provider)) {
                    game.providers.push(provider);
                }
                w88Mobile.v2.Slots.items.push(game);
            }
        });
    }

    function itemsByClub(providers, section) {
        return _.filter(w88Mobile.v2.Slots.items, function (item) {
            var itemProviders = _.join(item.providers, ",").toLowerCase().split(",");
            var hasClub = !_.isEmpty(_.intersection(itemProviders, providers));
            if (_.isUndefined(section)) return hasClub;
            else return _.includes(item.Section, section) && hasClub;
        });
    }

    function itemsBySection(section) {
        return _.filter(w88Mobile.v2.Slots.items, function (item) {
            return _.includes(item.Section, section)
        });
    }

    function toggleSection(self, section) {
        if (!$(self).hasClass('active')) {
            $('#sectionTab').find('li').removeClass('active')
            $(self).addClass('active');
        }

        w88Mobile.v2.Slots.section = section;

        pubsub.publish("fetchSlots", w88Mobile.v2.Slots.club);
    }


    function selectClub(clubName) {
        var club = _.find(w88Mobile.v2.Slots.clubs, function (data) {
            return _.isEqual(data.name.toLowerCase(), clubName);
        });

        w88Mobile.v2.Slots.club = club;
    }

    function translations(success, error) {
        var url = "/api/contents";
        send(url, "GET", {}, success, error);
    }

    function getFilterOptions(club) {
        translations(function (res) {
            $('#clubCategory').append($("<option></option>").attr("value", "all").text(res.ResponseData.LABEL_ALL_DEFAULT));

            _.forEach(club.providers, function (provider) {

                var url = "/api/games/" + provider + "/category";
                send(url, "GET", {}, function (response) {

                    _.forEach(response.ResponseData, function (data) {
                        if ($('#clubCategory').find('option[value="' + data.Value + '"]').length == 0) {
                            $('#clubCategory').append($("<option></option>").attr("value", data.Value).text(data.Text))
                        }
                    })

                }, function () { });
            })

            $('#clubMinBet').append($("<option></option>").attr("value", "all").text(res.ResponseData.LABEL_ALL_DEFAULT));

            _.forEach(club.providers, function (provider) {

                var url = "/api/games/" + provider + "/minbet";
                send(url, "GET", {}, function (response) {

                    _.forEach(response.ResponseData, function (data) {
                        if ($('#clubMinBet').find('option[value="' + data + '"]').length == 0) {
                            $('#clubMinBet').append($("<option></option>").attr("value", data).text(data))
                        }
                    })

                }, function () { });
            })

            $('#clubPlayLines').append($("<option></option>").attr("value", "all").text(res.ResponseData.LABEL_ALL_DEFAULT));

            _.forEach(club.providers, function (provider) {

                var url = "/api/games/" + provider + "/playlines";
                send(url, "GET", {}, function (response) {

                    _.forEach(response.ResponseData, function (data) {
                        if ($('#clubPlayLines').find('option[value="' + data + '"]').length == 0) {
                            $('#clubPlayLines').append($("<option></option>").attr("value", data).text(data))
                        }
                    })

                }, function () { });
            })

        }, function () { })

    }

    function showGameModal(id) {

        var game = _.find(w88Mobile.v2.Slots.items, function (data) {
            return _.isEqual(data.Id, id);
        });

        $('#gameImage').attr('src', game.ImagePath);

        $('#gameTitle').text(game.TranslatedTitle);

        $('#gameFunUrl').attr('href', game.FunUrl);

        if (w88Mobile.User.token) {
            $('#gameRealUrl').show();
            $('#gameRegisterUrl').hide();
            $('#gameLoginUrl').hide();

            $('#gameRealUrl').attr('href', game.RealUrl);
        }
        else {

            var loginUrl = "/_Secure/Login.aspx"
            var registerUrl = "/_Secure/Register.aspx"

            $('#gameRealUrl').hide();
            $('#gameRegisterUrl').show();
            $('#gameLoginUrl').show();

            $('#gameRegisterUrl').attr('href', loginUrl);
            $('#gameLoginUrl').attr('href', registerUrl);
        }

        $('#gameModal').modal('toggle');
    }
}

w88Mobile.v2.Slots = Slots();