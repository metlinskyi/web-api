## API

The hybrid REST/gRPC application
dotnet nuget add source "$PWD/../packages" --name local
dotnet add src/Api.csproj package Data.Entities --version 1.0.1 --source "$PWD/../packages" 

### Build
```bash
dotnet restore -s ../packages 
dotnet build src/api.csproj -c Release
```

### Running
dotnet ./src/bin/Release/net9.0/api
```bash
rm -rf app/* && dotnet publish src/api.csproj -c Release -o app/ \
    --runtime linux-x64 \
    --self-contained true \
    /p:PublishAot=true \
    /p:PublishTrimmed=true \
    /p:PublishSingleFile=true \
    /p:EnableCompressionInSingleFile=true 
```
```bash
dotnet run -p src/api.csproj
```

### Testing

```bash
dotnet test
```
protoc -I=/home/int/www.metlinskyi.com/api/src/Application MediatorService.proto --js_out=import_style=commonjs:/home/int/www.metlinskyi.com/api/src/

find . -type f -name '*.proto' -print0 | xargs -0 realpath | echo > $f


find . -name '*.proto' > $f | echo "$f"

find . -name '*.proto' -exec protoc -I=file --js_out=import_style=commonjs:. {} \;
export = DRI

find . -type f  -name '*.proto' -exec protoc -I=file --js_out=import_style=commonjs:. {} \;

&nbsp;
============
&copy; [The Best Software Engineer in The Universe!](https://www.linkedin.com/in/metlinskyi/)
