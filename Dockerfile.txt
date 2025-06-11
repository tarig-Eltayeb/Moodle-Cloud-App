# المرحلة الأولى: مرحلة البناء (استخدام .NET 8 SDK)
# نستخدم صورة مايكروسوفت الرسمية التي تحتوي على كل أدوات البناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# نسخ ملفات المشروع أولاً للاستفادة من التخزين المؤقت (caching)
COPY *.sln .
COPY MoodleTrueFalse/*.csproj ./MoodleTrueFalse/
RUN dotnet restore

# نسخ بقية ملفات المشروع
COPY . .

# بناء ونشر التطبيق في وضع الـ Release
RUN dotnet publish -c Release -o /app/publish

# ---

# المرحلة الثانية: مرحلة التشغيل النهائية (استخدام ASP.NET Runtime)
# نستخدم صورة أخف حجمًا وأكثر أمانًا تحتوي فقط على ما هو ضروري لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# نسخ المخرجات النهائية فقط من مرحلة البناء
COPY --from=build /app/publish .

# الأمر النهائي لتشغيل التطبيق
ENTRYPOINT ["dotnet", "MoodleTrueFalse.dll"]