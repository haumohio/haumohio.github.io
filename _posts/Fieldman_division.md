# Division of Responsibility
# Summary
There are different ways of deploying a field data collection system, such as ATLAS
FieldMan&trade;, within a large organisation, depending upon the amount of control that
central management wishes to have over the process.

It is possible for a company to gain centralised control over the process by installing
and using FieldMan at the central office, and simply distributing pre-loaded handhelds
to crews. Future work can be transmitted through the use of FieldMan Data
Interchange files, and the re-combination and checking work is preformed at the
central office.

Alternatively, crews can be empowered to manage, check, and re-combine jobs by
installing FieldMan at the crew manager level. Communication with the analysts
would be via complete job files (e.g. CDI files for ATLAS Cruiser&trade;). This provides
for a division along functional lines; collection is performed by field crews, and
analysis by Forest analysts. The process would need to be monitored and possibly
audited to ensure business rules were followed throughout.

# Centralised vs. distributed
## Distributed Control
This scenario divorces the Analyst from the process of data collection, with FieldMan
installed on each of the Field Crew Managers’ computers.

![Distributed Control](/docs/fm_division_1.png)
The Analyst designs the assessment in ATLAS Cruiser, creates a CDI file for it, and
sends it to the relevant Field Crew Manager. The Field Crew Manager is then
responsible for distributing the job amongst the field crews, re-combining the data,
and checking that it is complete and correct. The data can be edited by the Field Crew
Manager at this point. Once this process is complete, the CDI file is returned to the
Analyst.

This scenario would appeal to those companies that use a large number of contractors
or field crews, or where the field crews are largely grouped by region. The work of
processing is delegated to the Field Crew Managers, allowing the Analyst to
concentrate on business issues. The InboxDirect plugin could be used to aid the
distribution and collection of a large number of jobs being measured by a number of
remote Field Crew Managers.

### Centralised installation of a distributed process
It is possible to have centralised control of licensing and application versioning even
though the process is distributed, if the company has a network that can be accessed
by all of the concerned parties.

Both the licence for FieldMan and/or the actual program can be installed on a central
server and accessed by the Field Crew Managers through a network. Each user will
need to have the supporting frameworks installed on their computers (.Net and Java),
and will need to register the computer to use the licence setup on the central server.
In this configuration, a centralised IT group can manage licence renewal, software
upgrades, and database administration with little participation of the users.
The alternative is to install and licence FieldMan on each of the Field Crew
Managers’ computers with a fixed number of handheld devices prescribed.

## Centralised Control
This scenario involves the Analyst installing both the main ATLAS application (such
as Cruiser) and ATLAS FieldMan.

![Centralised Control](/docs/fm_division_2.png)
The Analyst creates the assessments in Cruiser, loads them into FieldMan and
distributes them via FDI files to the Field Crews through the Field Crew Managers.

Once the data is measured the Analyst then recombines and checks the data before
transferring it into Cruiser and analysing it. The Field Crew managers simply manage
the process of field collection, and have no part in re-combining, checking, or editing
field crew data.

This situation would appeal to those companies that desire top-level control of the
process and/or don’t have a large number of crews and jobs to process. The
CruiserDirect plugin could be used to aid the analyst in transferring data to and from
a local installation of Cruiser and FieldMan in an efficient manner.

# References
Other white papers in the series:
1. Controlling the data collection process with ATLAS FieldMan, Sep 2004.
1. Separating Context from Data, Dec 2004.