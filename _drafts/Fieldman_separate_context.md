# Separating Context from Data
# Summary
Context information is the information that is used to gather data, but is not the actual
measurements themselves, such as dictionaries or species sets.

Separation of the context from the data into a library provides the flexibility of being
able to construct new jobs without templates, and the speed of transferring only data
for standard assessments. It also facilitates standardisation of context information,
such as feature dictionaries, across a business.

Using context libraries does have some side effects that the user needs to be aware of.
Any time there are two copies of something there is a potential for them to get out of
synchronisation. A field crew’s copy of the library may become different to the
analyst’s library raising the possibility of data that references deleted context or
context that has been changed.

ATLAS products try to minimise these issues by embedding context into the data in
some cases, and considering the analyst’s library as the master library. The field
crew’s library can only be updated by copying it from the master library, and cannot
be modified in the field.

# What is context?
Context information is defined here as information that is used to gather data, or is
referenced by the data, but is not the actual measurements themselves.

Take the case of a field crew measuring trees. The diameters, heights, and branch size
are all examples of measurement data. The list of species that may be chosen from or
the dictionary for classifying features is the context in which the data is measured.
Anything that doesn’t change from assessment to assessment (as a rule) is context
information.

# Why separate?
Two of the main reasons for separation are flexibility and speed. That is, the
flexibility to create new jobs in the field, and the speed of transferring only data and
data designs between analysts and field crews.

## Data transferral
![Transferring data between sites with libraries](/docs/fm_context_1.png)
By separating the context information from the measurement data, regular data
collection is made more efficient. The context information need only be transferred
once for all standard jobs, and from then on only the design need be sent and the
measurements received. Overall, this gives a lightweight solution to the situation.
When the data is measured the measurement program references the library to provide
lists of species, features, user variables, and any other context information needed to
measure the data in the intended manner.

## Creation of new assessments in the field
The flexibility of separating context from data can be illustrated by the ability to
create new assessments while in the field.

![Creating a job in the Field](/docs/fm_context_2.png)
Once the library used by the field crew has been populated by the relevant master
library, there is no reason why the field crew can’t create a new assessment with
reference to the loaded context information. This could be done in the most remote
regions without any need to return to base to design the assessment. The request
could quite easily be made over a cell phone or radio to measure an area over the next
ridge or valley.

If the context was not separated the field crew would need to "cheat" the system by
keeping a number of possible "templates", or unmeasured assessments that could be
copied and populated. This scenario could lead to out-of-date context, or context that
is inconsistent across field crews.

## Organisation of Context Libraries
![Organisation of context into a library](/docs/fm_context_3.png)
There is also a feature of context separation that should not be overlooked. That is the
benefit of organisation and standardisation within a business.

By virtue of collating all of the context information and keeping it in the same
repository, the context information is sorted and rationalised. Duplicate definitions
can be easily identified and removed, and definitions of features can be agreed upon
throughout the business, improving communication of the actual meaning of the data.

# Issues to be aware of
## Distributed _same_ libraries
There is always a risk with having the same information in more than one place that
some data will be changed in one repository and not the other(s). This risk is no
greater using libraries than if the data was stored locally in each document, in fact is
less because there is a smaller number of copies of the context information.

## Version clashes
A clash of versions of context is a logical extension of the distributed _same_ libraries
issue.

Consider the case where the analyst has designed a dictionary for use in the field, but
while a job is being measured decides that one of the features should require a value
to be entered. The analyst changes the dictionary and then creates a new job that uses
the new version of the dictionary, and sends it out to be measured.

Changing the dictionary has created two possible versions of the dictionary; the one
that is currently being used to complete the previous job, and one that is attached to
the new job. Both jobs refer to the _same_ dictionary, but expect subtly different
content.

## Orphaned data
Data can be _orphaned_ when the context that it refers to is deleted from one or more
libraries.

Consider the case where the analyst has designed a dictionary for use in the field, but
while a job is being measured decides that one of the features is unnecessary and
removes it. If the analyst then created a new job the uses the new version of the
dictionary, a version clash would result. However, there is another issue when the
previous job is loaded back into the main program. There may be features in the data
that reference the feature definition in the context library that was deleted.

# How ATLAS systems use context libraries
## Cruiser objects and FieldMan context libraries
Both Cruiser and FieldMan have separate context libraries. The Cruiser context
library is considered the master library, and the FieldMan library is a distributed copy
of portions of it. Therefore, there is no facility in FieldMan to edit the context
information, which would inevitably lead to version clashes.

To prevent version clashes between loaded jobs, FieldMan does not accept any
attempts to add context to the library that has the same name as context that is already
in the library. A message is displayed that a job is using context that is different than
that which is already loaded and the job is not accepted. The analyst then needs to
rename the context or use the old version.

## Data Interchange files
ATLAS data interchange files usually carry the context definitions inside them as a
backup in case the context is not yet present in the destination library. This alleviates
the problem of orphaned data.

### CDI - Cruiser Data Interchange
This file may contain any Cruiser object, including Assessments and the required
context objects that the Assessment needs, and is used to transfer data between
different installations of ATLAS programs. To create a CDI file, select "Write CDI"
from the File menu in Cruiser. If the CDI file is intended for FieldMan, only the
Assessment needs to be specified (one per CDI file), and the required context
references are added automatically.

### SDI - SilviQC Data Interchange
This file is used to transfer data primarily between SilviQC and FieldMan. It includes
some context information, as well as the design and data for assessments. A SDI file
for the active QC in SilviQC can be created by selecting "Save Template" from the
file menu.
### FDI - FieldMan Data Interchange
This file is used to send data to and from FieldMan on the PC and FieldMan on the
handheld computer. This file may contain context, or may contain just references to
context depending on what currently exists in the respective context libraries. Note
that there is a copy of the relevant parts of the context library stored on each handheld
computer.

## Changing definitions
When changing any definitions in the context library in Cruiser it is always
recommended that a copy of the context is made and named with an appropriate
different name. The changes can then be made to the copy.

For example to change the dictionary called _NZ Native_ so that feature __A__ now
requires a value, the analyst should make a copy of the dictionary and call it _NZ
Native 2_, or maybe _NZ Native Aug 2005_, and then change the feature in the copy.
Any new jobs created from then on would use the new version of the dictionary, and
any old jobs can still use the old version. When the new job is sent to FieldMan for
measurement there will be no clashes of context.

FieldMan does not allow changes to the context, and provides no methods for editing
the context. All context is defined by Cruiser (or whichever master ATLAS
application is relevant), and is considered merely a local copy.
