## Gaps and Assumptions

During the implementation of the Aurora University system, the following gaps in the requirements were identified, and certain assumptions were made to make the system functional:


### 1. Seminar Groups and Allocation
- **Gap:** Capacity limits are defined, but no advanced constraints such as student or staff priorities, online vs. in-person, or preferences were specified.
- **Assumption:** Students are allocated to groups in a fair first-fit order, without considering preferences.

### 2. Staff Responsibilities
- **Gap:** Staff may teach multiple sessions, but limits on overlapping teaching assignments or maximum workload are not defined.
- **Assumption:** Staff can manage multiple modules and sessions without conflict checks beyond session overlap.

### 3. Class Modeling Challenge
- **Challenge:** The requirement to create a minimal but complete set of four C# classes presented a design challenge.  
- **Observation:** If we strictly limited ourselves to only four classes, the data would become highly denormalized. Relationships would be forced into a single class or stored in an inconsistent way, making queries and updates more complex.  

