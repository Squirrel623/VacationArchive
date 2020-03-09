TS=2
readonly TS

dotnet cs2ts -c --import-generation "Simple" --tab-size $TS -o "./web/generated-types/api/vacation" "./server/Controllers/Vacation/ApiModels"
dotnet cs2ts -c --import-generation "Simple" --tab-size $TS -o "./web/generated-types/api/user" "./server/Controllers/User/ApiModels"