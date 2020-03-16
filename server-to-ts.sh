TS=2
readonly TS

rm -r ./web/generated-types

# we have to process this file by file because if you give it a directory it will put import types from
# subdirectories in the output directory, which we don't want
process_directory() {
  input_dir=$1
  output_dir=$2

  for file in "${input_dir}/"*.cs
  do
    dotnet cs2ts --import-generation "Simple" --tab-size $TS -o "${output_dir}" "$file"
  done
}

process_directory "./server/Controllers/Vacation/ApiModels" "./web/generated-types/api/vacation"
process_directory "./server/Controllers/Vacation/ApiModels/Activities" "./web/generated-types/api/vacation/activity"
process_directory "./server/Controllers/Vacation/ApiModels/Activities/Media" "./web/generated-types/api/vacation/activity/media"
process_directory "./server/Controllers/User/ApiModels" "./web/generated-types/api/user"
