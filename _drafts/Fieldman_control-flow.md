# Controlling the data collection process with ATLAS FieldMan&trade;
# Rationale
It is one of the aims of the ATLAS FieldMan project to provide a transparent structure for
controlling the process of collecting field data. Part of that structure is the delegation of
authority to perform certain data collection actions to the different data collection roles. The
three main roles used in the FieldMan structure are:

* The Data Owner. This person owns an ATLAS application (e.g. ATLAS Cruiser&trade;) and
is interested in getting some real data to analyse. This person may or may not have a
number of device licences as part of the ATLAS application contract.
* The Field Manager. This person may be a contractor or a staff member in charge of data
collection, and owns a copy of ATLAS FieldMan. This person may have purchased a
number of device licences, plus any licences that have been given from a Data Owner.
* The Field Crew. This person is responsible for going into the field and actually collecting
the data. Each field crew owns a handheld computer with ATLAS FieldMan installed on
it. Each handheld computer may have a number of device licences assigned to it from the
Field Manager.

## Security
All data that is collected through ATLAS FieldMan is encrypted to keep the actual data secure
from unauthorised eyes. This means that if a Data Owner requests that data is collected, there is
an assurance that only the authorised Field Manager in charge of managing the data collection
process and the appropriate authorised Field Crew(s) are able to read and edit the data.

##Transparent transfer of responsibility
Device licences clearly indicate who has responsibility to collect ATLAS data. Through the
introduction of Device Licence Certificates, there is no confusion about who has the rights to
collect the data, and for whom. By company A assigning a Device Licence Certificate to
contractor B, it is obvious that company A has delegated the task of collecting data to contractor
B. In addition contractor B is unable to use company A’s licence to collect data for another
company.

## Non-proliferation of unlicensed programs
Lastly, licensing is used to protect ATLAS Technology from malicious users copying the
software and using it without an agreed contract.

# The basics - Getting licensed to operate
## Desktop Sheriff Licence

![Licensing](/docs/fm_control_1.png)
ATLAS uses the Sheriff software protection system to ensure
that the software is not pirated. The basic Sheriff system is
described in a diagram from their website.

After the installation process is complete, the user runs the
Sheriff "Licence Administrator" program, and either generates
a new licence following the process in the diagram, or
registers with an existing multi-user licence on a network.

Once the user is licensed, a detailed properties file can be
accessed, which holds specific information about the models and features that may be used
within the ATLAS product(s) installed. Included in these details is the number of Device
Licences purchased, determining the size of the Device Licence Pool.
![CENTERED](/docs/fm_control_2.png)

## Device Licences
Once the software is installed and licensed, and the Device Licence Pool is established, Device
Licences may be assigned from the pool to devices. A device is any compatible handheld
computer used by a field crew, such as an Allegro or a Husky.
![CENTERED](/docs/fm_control_3.png)

By assigning a Device Licence to a device, the device is given permission to gather data for that
application (eg Cruiser) until the licence's expiry date.

Device Licences may be revoked from a device, and re-assigned at will. The total number of
Device Licences in the pool plus those assigned to devices is always fixed by the number in the
licence properties file.

# Advanced - Transfer power to 3rd parties
The basic scenario as outlined above is fine for companies that own Cruiser, and FieldMan, and
do their own data collection in the field, with their own handheld computers. But for a lot of
cases, it gets a little more complicated than that. Contractors could be used to gather the data,
who may or may not have the capability or the willingness to run their own copy of FieldMan.

## Direct licensing of 3rd party devices
This option is for those companies who have a contractor who:
* Does the data collection
* Owns the handheld computers, or uses the company's handheld computers
* Collects FieldMan data just for this company
* Doesn't have a copy of FieldMan

This option is very similar to the basic operation, and could also be used by contractors with
many remote field crews.

It requires that each of the handheld computers is physically connected to the Data Owner’s
desktop running FieldMan once, for assigning the licence. The device is connected to the
desktop, and assigned a Device Licence from the pool as per normal. This forms a primary
relationship between the desktop and the handheld.

Once licensed each data collection job may be emailed to the user of the handheld through the
import/export feature of FieldMan.

## Transfer of device licences via certificates
This option is for those companies who have a contractor who:
* Does the data collection
* Owns their own handheld computers
* Collects data for one or more companies
* Has a copy of FieldMan, with the 'Multi-Owner' feature enabled
![CENTERED](/docs/fm_control_4.png)

In essence the Data Owner can transfer the rights to use one or more of its Device Licences to
another user of FieldMan. To do this, the Data Owner generates a Device Licence Certificate
file, and emails it to the contractor to add to the contractor's pool. The contractor can then assign
the licence to a device, as if there was an extra Device Licence in the pool.

Certificates do have a couple of extra restrictions:
* They expire at a time designated by the creator of the certificate. This allows the Data Owner to give a contractor rights to collect data for a fixed term.
* Data can only be collected for the creator of the certificate. This means the data collected is tagged as being owned by the certificate creator, stopping a contractor from collecting data for company 'B' using company 'A's licence.

By using certificates a contractor could get a job completed more quickly by enabling an extra
field crew to collect data for a client, or even enable a contractor to collect data for an application the contractor doesn't have any licence to collect for at all.

# Glossary

* Device - Any compatible handheld computer used by a field crew, such as an Allegro or a Husky.
* Device Licence - A single unit of permission to gather data on a single device, for a single application (such as Cruiser), until an expiry date. Possibly also restricted to a single data owner.
* Device Licence Pool - the pool of available Device Licences that may yet be distributed amongst the available Devices.
* Sheriff - A third party software protection tool used by ATLAS applications to prevent piracy.
* Licence Properties File - A file containing detailed information about which features the user has purchased within the ATLAS software.
* Data Owner - The company that the data is being collected for. This company is deemed the owner of the data, and is usually a user of an ATLAS application such as Cruiser.

# References
1. Sheriff Software, "http://www.sheriff-software.com", 2004.