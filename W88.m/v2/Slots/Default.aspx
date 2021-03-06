﻿<%@ Page Language="C#" Inherits="v2_Slots_Default" CodeFile="Default.aspx.cs" AutoEventWireup="true" %>

<%--<%  var club = (RouteData.Values["club"] != null) ? RouteData.Values["club"] : ""; %>--%>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>Slots</title>
    <!-- Bootstrap -->
    <link href="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/css/base.css" rel="stylesheet">
    <link href="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/css/styles.css" rel="stylesheet">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div id="page-stack" style="display: none"></div>
    <!-- Game Modal -->
    <div class="modal fade" id="gameModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content modal-transparent modal-game">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span class="icon icon-close"></span></button>
                </div>
                <div class="modal-body text-center">
                    <h4 id="gameTitle"></h4>
                    <div class="clearfix modal-game-row">
                        <div class="modal-game-col modal-game-col-thumb">
                            <div class="modal-game-thumb">
                                <img id="gameImage" src="" alt="">
                            </div>
                        </div>
                        <div class="modal-game-col modal-game-col-btns">
                            <a href="#" id="gameFunUrl" class="btn btn-block btn-default" target="_blank">Try Now</a>
                            <a href="#" id="gameRegisterUrl" class="btn btn-block btn-gray" hidden>Join Now</a>
                            <a href="#" id="gameLoginUrl" class="btn btn-block btn-primary" hidden>Login</a>
                            <a href="#" id="gameRealUrl" class="btn btn-block btn-gray" target="_blank">Play Now</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Filter Modal -->
    <div class="modal modal-fullscreen fade" id="filterModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span class="icon icon-close"></span></button>
                    <h4 class="modal-title">Filter Bravado</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label id="filter-category-label">Category</label>
                        <select name="" id="clubCategory" class="form-control"></select>
                    </div>
                    <div class="form-group">
                        <label id="filter-minbet-label">Min. Bet (USD)</label>
                        <select name="" id="clubMinBet" class="form-control"></select>
                    </div>
                    <div class="form-group">
                        <label id="filter-lines-label">Play Lines</label>
                        <select name="" id="clubPlayLines" class="form-control"></select>
                    </div>
                    <div class="form-group">
                        <label id="filter-providers-label">Providers</label>
                        <select name="" id="clubProviders" class="form-control"></select>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id="filter-apply-button" type="button" class="btn btn-primary btn-block" data-dismiss="modal" aria-label="Apply" onclick="filterGames()">Apply</button>
                </div>
            </div>
        </div>
    </div>

    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/cookie.js"></script>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/jquery-2.2.4.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/bootstrap.min.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/amplify.min.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/lodash.min.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/pubsub.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/slots/settings/<%=SlotSettingsFile %>.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/jquery.i18n.min.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/jquery.i18n.messagestore.min.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/jquery.i18n.fallbacks.min.js"></script>
    <script type="text/javascript">
        w88Mobile = {};
        w88Mobile.v2 = {};
        w88Mobile.v2._templates = {};
        window.User = w88Mobile.User = {};
        w88Mobile.User.token = Cookies().getCookie('s');
        w88Mobile.User.hasSession = !_.isEmpty(w88Mobile.User.token);
        w88Mobile.User.lang = Cookies().getCookie('language');
        window.User.storageExpiration = { expires: 300000 };
        String.prototype.toCapitalize = function () {
            return this.charAt(0).toUpperCase() + this.slice(1);
        }
    </script>    
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/constants.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/growl.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/slots/shared/templates.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/slots/shared/slots.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/i18n/contents-<%=commonVariables.SelectedLanguageShort%>.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/translate.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/loader.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/slots/ctrl/slotsCtrl.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/slots/ctrl/clubsCtrl.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/slots/ctrl/launcherCtrl.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/modules/slots/shared/routes.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/usercheck.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/router.js?v=<%=ConfigurationManager.AppSettings.Get("scriptVersion") %>"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/history.min.js"></script>
    <script src="<%=ConfigurationManager.AppSettings.Get("AssetsPath") %>/assets/js/vendor/history.adapter.jquery.min.js"></script>
    <script src="https://login.goldenphoenix88.com/jswrapper/integration.js.php?casino=blacktiger88" type="text/javascript"></script>
    <script type="text/javascript">
        // @todo remove this later if v2.Constants is no longer used
        w88Mobile.v2.Constants = _constants;
        w88Mobile.APIUrl = _constants.API_URL;
        w88Mobile.Growl.init();
        _.templateSettings = {
            interpolate: /\{\{(.+?)\}\}/g,      // print value: {{ value_name }}
            evaluate: /\{%([\s\S]+?)%\}/g,   // excute code: {% code_to_execute %}
            escape: /\{%-([\s\S]+?)%\}/g
        };
        _.templateSettings.variable = "slotData";
        var currentFilter = {};
        w88Mobile.Loader.init();

        // headers and routes
        pubsub.subscribe('contentsLoaded', onContentsLoaded);
        pubsub.subscribe("changeRoute", onChangeRoute);
        pubsub.subscribe("changeHeader", onChangeHeader);
        pubsub.subscribe('searchSlots', onSearchSlots);

        w88Mobile.v2._templates = new w88Mobile.v2.Templates();
        var _templates = w88Mobile.v2._templates; // alias for templates
        _templates.init();

        w88Mobile.v2._contents = new w88Mobile.Translate();
        var _contents = w88Mobile.v2._contents; // alias for contents
        _contents.init();

        var _routes = w88Mobile.v2.Routes;

        function filterClubSlots(tab, section) {
            if (!$(tab).hasClass('active')) {
                $('#sectionTab').find('li').removeClass('active')
                $(tab).addClass('active');
            }
            _routes.currentCtrl().filterClubSlots({
                section: section
            });
        }

        function onContentsLoaded(topic, data) {

            $(document).ready(function () {
                pubsub.publish("changeHeader");
                _routes.init();

                // fetch items
                _.forEach(w88Mobile.v2.Slots.clubs, function (club) {
                    _.forEach(club.providers, function (provider) {
                        w88Mobile.v2.Slots.get(provider, function (response) {
                            _.forEach(response.ResponseData.Games, w88Mobile.v2.Slots.formatReleaseDate);
                            w88Mobile.v2.Slots.addItems(response.ResponseData.Games, response.ResponseData.Provider);
                            if (response.ResponseCode == 1) {
                                try {
                                    _routes.currentCtrl().onSlotItemsChanged({
                                        provider: response.ResponseData.Provider
                                    });
                                } catch (e) {

                                }
                            }
                        }, function () { })
                    });
                });

                var History = window.History;
                if (!History.enabled) {
                    return false;
                }

                router.set_routes(_routes.slotRoutes());

                History.Adapter.bind(window, 'statechange', function () {
                    var State = History.getState();
                    router.route(State.url);
                });

                // catch all link clicks
                $('body').on('click', 'a', _routes.click_handler);

                // now use app.go(url) to move around
                router.route(router.get_path(window.location.href));

            });
        }

        /**
        * loop through slots
        * fetch every provider  (fetchSlots)
        * add games (addGames)
        * display Clubs (updateSlotList)
        */

        // should display category/clubs
        function onUpdateSlotList(topic, provider) {

            var clubs = _.filter(w88Mobile.v2.Slots.clubs, function (club) {
                return _.indexOf(club.providers, provider.toLowerCase()) !== -1;
            });

            if (_.isEmpty(clubs)) return;

            var section = "Home";

            _.forEach(clubs, function (club) {
                var items = w88Mobile.v2.Slots.itemsByClub(club.providers, section);
                games = _.slice(items, 0, w88Mobile.v2.Slots.getClubLimit(section));
                pubsub.publish("updateList", {
                    games: games
                    , club: club
                });
            });

        }

        function onFilterSlot(topic, filter) {
            var items = [];
            currentFilter = filter;
            if (!_.isUndefined(filter.club)) {
                items = w88Mobile.v2.Slots.itemsByClub(filter.club.providers);
            }
            if (_.isUndefined(filter.section)) {
                filter.section = _.first(w88Mobile.v2.Slots.sections);
            }
            items = _.filter(items, function (item) {
                var sections = _.join(item.Section, ",").toLowerCase().split(",");
                return _.includes(sections, filter.section.toLowerCase());
            });
            pubsub.publish("updateList", {
                games: items
                , club: filter.club
            });

            w88Mobile.v2.Slots.getFilterOptions(currentFilter.club);
        }

        function onAddSlots(topic, data) {
            w88Mobile.v2.Slots.addItems(data.ResponseData.Games, data.ResponseData.Provider);
            pubsub.publish("updateSlotList", data.ResponseData.Provider);
        }

        function onLoadSlots() {
            _.forEach(w88Mobile.v2.Slots.clubs, function (club) {
                pubsub.publish("fetchSlots", club);
            });
        }

        function onFetchSlots(topic, club) {
            _.forEach(club.providers, function (provider) {
                w88Mobile.v2.Slots.get(provider, function (data) {
                    slotList = data;
                    pubsub.publish('addSlots', data);
                }, function () { })

            })
        }

        function onChangeRoute(topic, data) {
            _routes.changeRoute(data.route, data.params);
        }

        function viewClubFilter() {
            var ctrl = _routes.currentCtrl();
            showFilterModal(ctrl.club);
        }

        function viewClub(club) {
            pubsub.publish("changeRoute", {
                route: "club", params: {
                    club: club
                }
            });

            w88Mobile.v2.Slots.club = club;
        }

        function viewGame(id, club, mode) {

            if (_.isUndefined(club)) {
                var ctrl = _routes.currentCtrl();
                club = (!_.isUndefined(ctrl.page.club)) ? ctrl.page.club : "";
            }

            pubsub.publish("changeRoute", {
                route: "launcher", params: {
                    gameId: id
                    , club: club
                    , mode: mode
                }
            });

            w88Mobile.v2.Slots.club = club;
        }

        function onUpdateList(topic, data) {
            var club = data.club
            , games = data.games;
            var content = _.template(_templates.SlotList);
            var innerHtml = content({
                games: games
                , club: club
            });
            if (!_.isEmpty(_routes.currentPage().find('.' + club.name + '-main'))) {
                _routes.currentPage().find('.' + club.name + '-main').html($(innerHtml).html());
            } else {
                _routes.currentPage().find(".main-content").append(innerHtml);
            }
        }

        function showFilterModal(club) {

            $("#filter-category-label").html(_contents.translate("LABEL_SLOTS_CATEGORY"));
            $("#filter-minbet-label").html(_contents.translate("LABEL_SLOTS_MINBET"));
            $("#filter-lines-label").html(_contents.translate("LABEL_SLOTS_LINES"));
            $("#filter-providers-label").html(_contents.translate("LABEL_SLOTS_PROVIDER"));
            $("#filter-apply-button").html(_contents.translate("LABEL_APPLY"));

            $('#filterModal').modal('toggle');
            $('.modal-title').html(_contents.translate(club.key + "_FILTER", club.title));
        }

        function filterGames() {

            var ctrl = _routes.currentCtrl();
            var category = $('#clubCategory').val();
            var minbet = $('#clubMinBet').val();
            var playlines = $('#clubPlayLines').val();
            var provider = $('#clubProviders').val();

            var form = {
                category: category
                , minbet: minbet
                , playlines: playlines
                , provider: provider
            }

            if (_routes.current() != "club_filter") {
                pubsub.publish("changeRoute", {
                    route: "club_filter",
                    params: {
                        club: ctrl.club.name
                        , form: form
                    }
                });
            } else {
                ctrl.filterClubSlots({
                    form: form
                });
            }
        }

        var searchToken;

        function showSearchHeader(route) {

            switch (route) {
                case 'club_search':
                    var club = _routes.currentCtrl().club;
                    pubsub.publish("changeRoute", {
                        route: route
                        , params: {
                            club: club.name
                        }
                    });
                    break;
                case 'index_search':
                    pubsub.publish("changeRoute", {
                        route: route
                        , params: {}
                    });
                    break;
            }
        }

        function onSearchSlots(topic, data) {
            var club = data.club
             , games = data.games;

            if (games.length > 0) {

                var content = _.template(_templates.SearchList);
                var innerHtml = content({
                    games: games
                    , club: club
                });
                if (!_.isEmpty(_routes.currentPage().find('.search-main'))) {
                    _routes.currentPage().find('.search-main').html($(innerHtml).html());
                } else {
                    _routes.currentPage().find(".main-content").html(innerHtml);
                }
            } else {
                _routes.currentPage().find(".main-content").children().remove();
            }
        }

        function onChangeHeader(topic, data) {
            var content = _.template(_templates.TopBar);

            var innerHtml = content({
                route: _routes.current()
            });
            var headerElem = _routes.currentPage().find(".header-main");
            headerElem.empty();
            headerElem.append(innerHtml);

            headerElem.find("icon.icon-back").click(function (e) {
                e.preventDefault();
            });

            $("#searchText").popover({
                trigger: "manual"
            });
            if (_.includes(_routes.currentPage().selector, "search")) {
                headerElem.addClass("header-search");

                $("#searchText").on("input click keyup paste propertychange", function () {
                    var hasPopOver = ($(this).next('div.popover:visible').length > 0);
                    if (_.isEmpty(this.value)) {
                        if (!hasPopOver)
                            $(this).popover('show');
                    } else {
                        if (hasPopOver)
                            $(this).popover('hide');
                    }

                    _routes.currentCtrl().filterSearchSlots({
                        title: this.value
                    });
                });

                $("#searchText").popover('show');
            }
            else
                headerElem.removeClass("header-search");

        }

        function clearSearchText() {
            $("#searchText").val('').trigger("change").focus();
        }

        if (inIframe()) {
            var parentOrigin = window.location.origin;
            var parentWindow = window.parent;

            parentWindow.postMessage('slot-home', parentOrigin);
        }

        function inIframe() {
            try {
                return window.self !== window.top;
            } catch (e) {
                return true;
            }
        }

        if (!inIframe()) {

            var launcherOrigin = window.location.origin;
            var launcherWindow = {};

            // Listen for the response from the child
            window.addEventListener('message', function (event) {

                // This 'if' block is optional, but good for security.
                if (event.origin !== launcherOrigin || event.source !== launcherWindow) {
                    throw new Error('The domain of the child does not match');
                }
                // remove floating button
                try {
                    Native.onSlotGameClosed();
                } catch (e) {
                    console.log(e.message)
                }
                switch (event.data) {
                    case "funds":
                        location.href = _constants.FUNDS_URL;
                        break;
                    case "home":
                        location.href = _constants.DASHBOARD_URL;
                        break;
                    case "slot-home":
                    default:
                        // remove launcher
                        _routes.previous();
                        break;
                }
            }, false);
        }


        $(window).bind('resize', function () {
            if (!_.isUndefined(_routes.currentCtrl().resize)) {
                _routes.currentCtrl().resize();
            }
        });

        // remove floating button
        try {
            Native.onSlotGameClosed();
        } catch (e) {
            console.log(e.message)
        }

    </script>
</body>
</html>
