[Setup]
AppName=Freestyle Cast / Crew Edit
AppId=FreestyleCastCrewEdit
AppVerName=Freestyle Cast / Crew Edit 3.0.1.3
AppCopyright=Copyright © Doena Soft. 2010 - 2022
AppPublisher=Doena Soft.
AppPublisherURL=http://doena-journal.net/en/dvd-profiler-tools/
DefaultDirName={commonpf32}\Doena Soft.\FreestyleCastCrewEdit
DefaultGroupName=Freestyle CastCrew Edit
DirExistsWarning=No
SourceDir=..\FreestyleCastCrewEdit\bin\x86\FCCE
Compression=zip/9
AppMutex=InvelosDVDPro
OutputBaseFilename=FreestyleCastCrewEditSetup
OutputDir=..\..\..\..\FreestyleCastCrewEditSetup\Setup\FreestyleCastCrewEdit
MinVersion=0,6.1sp1
PrivilegesRequired=admin
WizardStyle=modern
DisableReadyPage=yes
ShowLanguageDialog=no
VersionInfoCompany=Doena Soft.
VersionInfoCopyright=2010 - 2022
VersionInfoDescription=Freestyle Cast / Crew Edit Setup
VersionInfoVersion=3.0.1.3
UninstallDisplayIcon={app}\djdsoft.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Messages]
WinVersionTooLowError=This program requires Windows XP or above to be installed.%n%nWindows 9x, NT and 2000 are not supported.

[Types]
Name: "full"; Description: "Full installation"

[Files]
Source: "djdsoft.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.DVDProfilerHelper.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.DVDProfilerXML.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "FCCE.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "FCCE.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.FCCE.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.FCCE.pdb"; DestDir: "{app}"; Flags: ignoreversion

Source: "..\..\..\..\FreestyleCastCrewEditPlugin\bin\x86\FCCEplugin\DoenaSoft.FCCE.Plugin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\..\..\..\FreestyleCastCrewEditPlugin\bin\x86\FCCEplugin\DoenaSoft.FCCE.Plugin.pdb"; DestDir: "{app}"; Flags: ignoreversion

Source: "de\DoenaSoft.DVDProfilerHelper.resources.dll"; DestDir: "{app}\de"; Flags: ignoreversion

Source: "Readme\readme.html"; DestDir: "{app}\Readme"; Flags: ignoreversion

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\Freestyle CastCrew Edit"; Filename: "{app}\FCCE.exe"; WorkingDir: "{app}"; IconFilename: "{app}\djdsoft.ico"
Name: "{userdesktop}\Freestyle CastCrew Edit"; Filename: "{app}\FCCE.exe"; WorkingDir: "{app}"; IconFilename: "{app}\djdsoft.ico"

[Run]
Filename: "{win}\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe"; Parameters: "/codebase ""{app}\DoenaSoft.FCCE.Plugin.dll"""; Flags: runhidden

;[UninstallDelete]

[UninstallRun]
Filename: "{win}\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe"; Parameters: "/u ""{app}\DoenaSoft.FCCE.Plugin.dll"""; Flags: runhidden

[Registry]
; Register - Cleanup ahead of time in case the user didn't uninstall the previous version.
Root: HKCR; Subkey: "CLSID\{{07457294-7E9E-4A08-A129-79DCF283B2A1}"; Flags: dontcreatekey deletekey
Root: HKCR; Subkey: "DoenaSoft.DVDProfiler.FreestyleCastCrewEdit.Plugin"; Flags: dontcreatekey deletekey
Root: HKCU; Subkey: "Software\Invelos Software\DVD Profiler\Plugins\Identified"; ValueType: none; ValueName: "{{07457294-7E9E-4A08-A129-79DCF283B2A1}"; ValueData: "0"; Flags: deletevalue
Root: HKLM; Subkey: "Software\Classes\CLSID\{{07457294-7E9E-4A08-A129-79DCF283B2A1}"; Flags: dontcreatekey deletekey
Root: HKLM; Subkey: "Software\Classes\DoenaSoft.DVDProfiler.FreestyleCastCrewEdit.Plugin"; Flags: dontcreatekey deletekey
; Unregister
Root: HKCR; Subkey: "CLSID\{{07457294-7E9E-4A08-A129-79DCF283B2A1}"; Flags: dontcreatekey uninsdeletekey
Root: HKCR; Subkey: "DoenaSoft.DVDProfiler.FreestyleCastCrewEdit.Plugin"; Flags: dontcreatekey uninsdeletekey
Root: HKCU; Subkey: "Software\Invelos Software\DVD Profiler\Plugins\Identified"; ValueType: none; ValueName: "{{07457294-7E9E-4A08-A129-79DCF283B2A1}"; ValueData: "0"; Flags: uninsdeletevalue
Root: HKLM; Subkey: "Software\Classes\CLSID\{{07457294-7E9E-4A08-A129-79DCF283B2A1}"; Flags: dontcreatekey uninsdeletekey
Root: HKLM; Subkey: "Software\Classes\DoenaSoft.DVDProfiler.FreestyleCastCrewEdit.Plugin"; Flags: dontcreatekey uninsdeletekey

[Code]
function IsDotNET40Detected(): boolean;
// Function to detect dotNet framework version 2.0
// Returns true if it is available, false it's not.
var
dotNetStatus: boolean;
begin
dotNetStatus := RegKeyExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4');
Result := dotNetStatus;
end;

function InitializeSetup(): Boolean;
// Called at the beginning of the setup package.
begin

if not IsDotNET40Detected then
begin
MsgBox( 'The Microsoft .NET Framework version 4.0 is not installed. Please install it and try again.', mbInformation, MB_OK );
Result := false;
end
else
Result := true;
end;