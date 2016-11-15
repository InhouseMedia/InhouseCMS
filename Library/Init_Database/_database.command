mongoexport -d Inhouse -c Article -o Article.json -p 27017 --pretty
mongoexport -d Inhouse -c Article_Content -o Article_Content.json -p 27017 --pretty
mongoexport -d Inhouse -c Navigation -o Navigation.json -p 27017 --pretty
mongoexport -d Inhouse -c Config -o Config.json -p 27017 --pretty
mongoexport -d Inhouse -c Box -o Box.json -p 27017 --pretty
mongoexport -d Inhouse -c User -o User.json -p 27017 --pretty