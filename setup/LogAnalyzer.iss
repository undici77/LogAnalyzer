#define ApplicationName 'LogAnalyzer'
#define ApplicationVersion  GetStringFileInfo('..\LogAnalyzer\bin\Release\LogAnalyzer.exe', PRODUCT_VERSION)

[Files]
Source: "..\LogAnalyzer\bin\Release\LogAnalyzer.exe"; DestDir: {app}; Flags: ignoreversion replacesameversion restartreplace; Permissions: everyone-full
Source: "Default\LogAnalyzerPattern.ini"; DestDir: {app}; Flags: ignoreversion onlyifdoesntexist; Permissions: everyone-full

[Dirs]
Name: "{app}"; Permissions: everyone-full

[Setup]
AppName={#ApplicationName}
AppVersion={#ApplicationVersion}
AppVerName={#ApplicationName} {#ApplicationVersion}
OutputBaseFilename={#ApplicationName}_Setup_{#ApplicationVersion}
AppPublisher=Alessandro Barbieri
AppPublisherURL=https://github.com/undici77/LogAnalyzer.git
DefaultDirName={commonpf64}\Undici77\{#ApplicationName}
DefaultGroupName=Undici77\{#ApplicationName}
Compression=lzma
SolidCompression=yes
MinVersion=6.1.7600
PrivilegesRequired=admin
AppCopyright=Copyright (C) 2021 Alessandro Barbieri
SetupIconFile=..\LogAnalyzer\res\LogAnalyzer.ico
UninstallDisplayIcon={app}\LogAnalyzer.exe
LicenseFile=License\license.rtf

[Icons]
Name: "{group}\LogAnalyzer"; Filename: "{app}\LogAnalyzer.exe"
