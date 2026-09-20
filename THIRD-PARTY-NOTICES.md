# Third-party resources and dependencies

Third-party components retain their respective licenses. This notice does not grant a license to the Reapify source code.

## Reapify brand

The Reapify name, logos, and brand identity belong to the project author. Their inclusion does not grant trademark rights or permission to reuse the brand.

## Interface and fonts

| Resource | Version | License and notice |
|---|---|---|
| Bootstrap, bundled locally | 5.1.0 | MIT; LICENSE in Advertisements/wwwroot/lib/bootstrap |
| jQuery | Bundled version in wwwroot/lib | MIT; LICENSE.txt included |
| jQuery Validation | Bundled version in wwwroot/lib | MIT; LICENSE.md included |
| jQuery Validation Unobtrusive | Bundled version in wwwroot/lib | MIT; LICENSE.txt included |
| Feather Icons, CDN | 4.29.0 | MIT; licenses/Feather-MIT.txt |
| Simple-DataTables, CDN | 7.1.2 | LGPL 3; licenses/Simple-DataTables.txt; source: https://github.com/fiduswriter/simple-datatables/tree/v7.1.2 |
| Poppins, PDF font | Existing TTF files | SIL OFL 1.1; Advertisements/wwwroot/assets/fonts/poppins/OFL.txt; upstream: https://github.com/google/fonts/tree/main/ofl/poppins |

The application loads the unmodified Simple-DataTables distribution separately from its own scripts. Its source and license remain available at the versioned upstream link above. Preserve its license and attribution; any redistributed or modified library files remain subject to the LGPL terms.


## .NET dependencies

- ClosedXML 0.105.0: MIT.
- Dapper 2.1.66: Apache-2.0.
- Microsoft.Data.SqlClient 6.1.1: MIT.
- QuestPDF 2025.7.3: terms included in licenses/QuestPDF-2025.7.3.md. The application selects Community. The package license describes eligibility for educational/open-source use and businesses below its revenue threshold. Check the applicable terms for future commercial deployments; not every use is unconditionally covered by MIT.

The direct and transitive package inventory is in licenses/NUGET-INVENTORY.md. Packages are restored through NuGet.

